using CarInfoCommon.Models;

namespace CarInfoTelegramBot.Services
{
    public interface IMessageProcessor
    {
        void Save(CarInfo carInfo);
        CarInfo Load(string id);
    }
}