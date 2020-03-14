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

        public void Save(CarInfo carInfo)
        {
            _carInfoRepository.Save(carInfo);
        }

        public CarInfo Load(string id)
        {
            var carInfo = _carInfoRepository.GetCarInfo(id);
            return carInfo;
        }
    }
}