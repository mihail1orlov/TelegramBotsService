using System;
using System.Net;
using System.Threading.Tasks;
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

            TranslateText("стул", "ru|en");
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

        private string TranslateText(string input, string languagePair)
        {
            string url = $"http://www.google.com/translate_t?hl=en&ie=UTF8&text={input}&langpair={languagePair}";

            WebClient webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };

            string result = webClient.DownloadString(url);

            // todo: StringComparison.InvariantCulture need to clarify
            result = result.Substring(result.IndexOf("id=result_box", StringComparison.Ordinal) + 22,
                result.IndexOf("id=result_box", StringComparison.Ordinal) + 500);

            result = result.Substring(0, result.IndexOf("</div", StringComparison.Ordinal));

            return result;
        }
    }
}
