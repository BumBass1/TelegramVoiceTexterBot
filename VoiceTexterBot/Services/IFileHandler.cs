using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceTexterBot.Services
{
    public interface IFileHandler
    {

        //скачиваем файл 
        Task Download(string fileId, CancellationToken ct);
        string Process(string param);
    }
}
