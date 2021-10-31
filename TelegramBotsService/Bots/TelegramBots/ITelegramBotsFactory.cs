using ServiceCommon;

namespace TelegramBots
{
    public interface ITelegramBotsFactory
    {
        IService GetCarInfoService(string token);
        IService GetEnglishService(string token);
        IService GetGitHubNotificatorService(string token);
        IService GetAvtoCarDriveService(string token);
    }
}