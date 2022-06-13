using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceTexterBot.Configuration
{
    public class AppSettings
    {
        //токен TL API
        public string BotToken { get; set; }


        // папка загрузки файлов
        public string DownloadsFolder { get; set; }

        // имя файла при загрузке

        public string AudioFileName { get; set; }

        // формат аудио при загрузке

        public string InputAudioFormat { get; set; }

        public string OutputAudioFormat { get; set; }

        public float InputAudioBitate { get; set; }




    }
}
