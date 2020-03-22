using System.Threading.Tasks;
using CarInfoCommon.Models;
using CarInfoDbService;

namespace CarInfoTelegramBot.Services
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly ICarInfoRepository _carInfoRepository;

        public MessageProcessor(ICarInfoRepository carInfoRepository)
        {
            _carInfoRepository = carInfoRepository;
        }

        public async Task<string> Process(string text, long id)
        {
            string message;

            if (string.Equals(text, "start"))
            {
                // todo: fake
                var carInfo = Load("72B0DF11A044482EB1568BFA289E6800");
                message = "Mileage: " + carInfo.Mileage;
            }
            else if (int.TryParse(text, out var distance))
            {
                var carInfo = Load("72B0DF11A044482EB1568BFA289E6800");
                carInfo.Mileage = distance;
                Save(carInfo);
                message = "Your data was save";
            }
            else
            {
                message = "Error!\nInvalid input format";
            }

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