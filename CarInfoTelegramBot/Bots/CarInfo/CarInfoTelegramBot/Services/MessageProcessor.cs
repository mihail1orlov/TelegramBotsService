using System.Threading.Tasks;
using CarInfoCommon.Models;
using CarInfoDbService;
using LoggerCommon;

namespace CarInfoTelegramBot.Services
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly ICarInfoRepository _carInfoRepository;
        private readonly ILogger _logger;

        public MessageProcessor(ICarInfoRepository carInfoRepository, ILogger logger)
        {
            _carInfoRepository = carInfoRepository;
            _logger = logger;
        }

        public async Task<string> Process(string text, long id)
        {
            //
            //
            _logger.Info($"{nameof(Process)}|start");
            string message;

            var carInfo = Load("72B0DF11A044482EB1568BFA289E6800");
            if (carInfo == null)
            {
                carInfo = new CarInfo(44);
                carInfo.Id = "72B0DF11A044482EB1568BFA289E6800";
                Save(carInfo);
            }

            if (string.Equals(text, "start"))
            {
                // todo: fake
                carInfo = Load("72B0DF11A044482EB1568BFA289E6800");
                message = "Mileage: " + carInfo.Mileage;
            }
            else if (int.TryParse(text, out var distance))
            {
                carInfo = Load("72B0DF11A044482EB1568BFA289E6800");
                carInfo.Mileage = distance;
                Save(carInfo);
                message = "Your data was save";
            }
            else
            {
                message = "Error!\nInvalid input format";
            }

            _logger.Info($"{nameof(Process)}|{nameof(message)}: {message}");
            return message;
        }

        private void Save(CarInfo carInfo)
        {
            _carInfoRepository.Save(carInfo);
        }

        private CarInfo Load(string id)
        {
            var carInfo = _carInfoRepository.GetCarInfo(id);
            return carInfo;
        }
    }
}