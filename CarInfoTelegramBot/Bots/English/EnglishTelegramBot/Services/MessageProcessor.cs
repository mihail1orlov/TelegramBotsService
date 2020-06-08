using System.Threading.Tasks;
using BotCommon;
using Castle.Core.Logging;
using EnglishCommon.Models;
using EnglishDbService;

namespace EnglishTelegramBot.Services
{
    public class EnglishMessageProcessor : IMessageProcessor
    {
        private readonly EnglishRepository _englishRepository;
        private readonly ILogger _logger;

        public EnglishMessageProcessor(EnglishRepository EnglishRepository, ILogger logger)
        {
            _englishRepository = EnglishRepository;
            _logger = logger;
        }

        public async Task<string> Process(string text, long id)
        {
            _logger.Info($"{nameof(Process)}|start");
            string message;

            if (string.Equals(text, "start"))
            {
                // todo: fake
                var english = Load("72B0DF11A044482EB1568BFA289E6800");
                message = "Mileage: " + english.Mileage;
            }
            else if (int.TryParse(text, out var distance))
            {
                var english = Load("72B0DF11A044482EB1568BFA289E6800");
                english.Mileage = distance;
                Save(english);
                message = "Your data was save";
            }
            else
            {
                message = "Error!\nInvalid input format";
            }

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
    }
}
