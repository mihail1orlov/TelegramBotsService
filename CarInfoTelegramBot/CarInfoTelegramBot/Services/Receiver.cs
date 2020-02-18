using CarInfoCommon.Models;

namespace CarInfoTelegramBot.Services
{
    public class Receiver : IReceiver
    {
        private readonly IRepository _repository;

        public Receiver(IRepository repository)
        {
            _repository = repository;
        }

        public bool Message(CarInfo carInfo)
        {
            _repository.Save(carInfo);
            return true;
        }
    }
}