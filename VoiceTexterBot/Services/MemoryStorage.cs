using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceTexterBot.Models;

namespace VoiceTexterBot.Services
{
    internal class MemoryStorage : IStorage
    {
        // хранилище сессии
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();

        }
        public Session GetSession(long chatId)
        {
            // возвращаем сессию по ключу, если она сущетсвует
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            // созлаем и возвращаем новую сессии, если такой не было
            var newSession = new Session() { LanguageCode = "ru" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
