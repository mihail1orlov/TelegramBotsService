using CarInfoCommon.Models;

namespace CarInfoTelegramBot.Services
{
    public interface ITransmitter
    {
        CarInfo Load();
    }
}