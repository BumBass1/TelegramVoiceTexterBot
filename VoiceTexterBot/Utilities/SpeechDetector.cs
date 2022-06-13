using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VoiceTexterBot.Extensions;
using Vosk;


namespace VoiceTexterBot.Utilities
{
    public static class SpeechDetector
    {
        public static string DetectSpeech(string audioPath, float inputBitrate, string languageCode)
        {
            Vosk.Vosk.SetLogLevel(-1);
            var modelPath = Path.Combine(DirectoryExtension.GetSolutionRoot(), "Speech-models", $"vosk-models-small-{languageCode.ToLower()}");
            Model model = new(modelPath);
            return GetWords(model, audioPath, inputBitrate);
        }

        //метод для распознавания слов
        private static string GetWords(Model model, string audioPath, float inputBitrate)
        {
            //для распознавания передаем битрейт и используемую языковую модель
            VoskRecognizer rec = new(model, inputBitrate);
            rec.SetMaxAlternatives(0);
            rec.SetWords(true);

            StringBuilder textBuffer = new();

            using (Stream source = File.OpenRead(audioPath))
            {
                byte [] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (rec.AcceptWaveform(buffer, bytesRead))
                    {
                        var sentenceJson = rec.Result();

                        JObject sentenceObj = JObject.Parse(sentenceJson);
                        string sentence = (string)sentenceObj["text"];
                        textBuffer.Append(StringExtension.UppercaseFirst(sentence) + ".");

                    }
                }
            }
            //распознавание предложений
            var finalSentece = rec.FinalResult();
            //сохараняем в JSON-объект
            JObject finalSentenceObj = JObject.Parse(finalSentece);
            //собираем итоговый текст
            textBuffer.Append((string)finalSentenceObj["text"]);
            //возвращаем в виде строки
            return textBuffer.ToString();
        }
    }
}
