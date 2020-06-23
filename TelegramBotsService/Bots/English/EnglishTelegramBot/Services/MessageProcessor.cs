using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using EnglishCommon.Models;
using EnglishDbService;
using LoggerCommon;

namespace EnglishTelegramBot.Services
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IEnglishRepository _englishRepository;
        private readonly ILogger _logger;

        public MessageProcessor(IEnglishRepository englishRepository, ILogger logger)
        {
            _englishRepository = englishRepository;
           _logger = logger;
        }

        public async Task<string> Process(string text, long id)
        {
            _logger.Info($"{nameof(Process)}|start");
            var message = string.Empty;
            message = Translate("стул");
            _logger.Info($"{nameof(Process)}|{nameof(message)}: {message}");
            return message;
        }

        private void Save(EnglishExercise english)
        {
            _englishRepository.Save(english);
        }

        private EnglishExercise Load(string id)
        {
            var english = _englishRepository.GetEnglishExercise(id);
            return english;
        }

        private string Translate(string word)
        {
            var toLanguage = "en";//English
            var fromLanguage = "ru";
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }
    }
}
