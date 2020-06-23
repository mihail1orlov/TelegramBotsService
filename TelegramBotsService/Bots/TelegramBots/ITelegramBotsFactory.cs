using ServiceCommon;

namespace TelegramBots
{
    public interface ITelegramBotsFactory
    {
        IService GetCarInfoService(string token);
        IService GetEnglishService(string token);
    }
}