using CarInfoCommon.Models;

namespace CarInfoTelegramBot.Services
{
    public interface IRepository
    {
        void Save(CarInfo carInfo);
    }
}