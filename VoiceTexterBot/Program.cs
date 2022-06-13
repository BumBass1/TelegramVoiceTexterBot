using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using VoiceTexterBot.Configuration;
using VoiceTexterBot.Controllers;
using VoiceTexterBot.Services;

namespace VoiceTexterBot
{ 
    public class Program
    {
        public static async Task Main ()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // отвечающий за жизненный цикл
            var host = new HostBuilder()
                .ConfigureServices((HostContext, services) => ConfigureServices(services)) // задаем конфигурацию
                .UseConsoleLifetime() // позволяем поддерживать активным приложением в консоле
                .Build(); //собираем

            Console.WriteLine("Сервис запущен");

            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }
        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            // контроллеры
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<IStorage, MemoryStorage>();
            //токе подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            // регистрируем постоянно активный сервис
            services.AddHostedService<Bot>();

            services.AddSingleton<IFileHandler, AudioFileHandler>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                DownloadsFolder = "C:\\Users\\BumBass\\down",
                BotToken = "5322361796:AAEGaDdjK98x3BNXp77Ipx9zogam1_PTgT0",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
                OutputAudioFormat = "wav",
            };
        }

       
    }
}
