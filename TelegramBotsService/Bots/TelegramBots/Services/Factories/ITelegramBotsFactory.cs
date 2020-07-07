using ServiceCommon;
using TelegramBots.Entities;

namespace TelegramBots
{
    /// <summary>
    /// Provides a factory that allows you to get an instance of <see cref= "IService"/> that
    /// is responsible for the basic functionality of the necessary telegram bot.
    /// </summary>
    public interface ITelegramBotsFactory
    {
        /// <summary>
        /// Gets the necessary telegram bot.
        /// </summary>
        /// <param name="token">The ID of the bot.</param>
        /// <param name="telegramBot">Type of the necessary telegram bot.</param>
        /// <returns>Necessary telegram bot.</returns>
        IService GetTelegramBot(string token, TelegramBot telegramBot);
    }
}