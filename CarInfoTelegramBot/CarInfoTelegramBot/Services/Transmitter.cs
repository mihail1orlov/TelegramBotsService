using CarInfoCommon.Models;

namespace CarInfoTelegramBot.Services
{
    public class Transmitter : ITransmitter
    {
        private readonly IRepository _repository;

        public Transmitter(IRepository repository)
        {
            _repository = repository;
        }

        public CarInfo Load()
        {
            var carInfo = _repository.Load();
            return carInfo;
        }
    }
}