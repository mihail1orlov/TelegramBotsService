using System;
using CommonServices;
using ServiceCommon;
using Telegram.Bot;
using TelegramBots.Entities;

namespace TelegramBots.Services.Factories
{
    /// <summary>
    /// Provides a factory that allows you to get an instance of <see cref= "IService"/> that
    /// is responsible for the basic functionality of the necessary telegram bot.
    /// </summary>
    public class TelegramBotsFactory : ITelegramBotsFactory
    {
        // Private fields
        private readonly IMessageProcessorFactory _messageProcessorFactoryBase;

        /// <summary>
        /// Creates Instance of <see cref="TelegramBotsFactory"/>
        /// </summary>
        /// <param name="messageProcessorFactory">Provides a factory that allows you to get an instance of <see cref= "IMessageProcessor"/>
        /// that is responsible for the basic functionality of the desired bot.</param>
        public TelegramBotsFactory(IMessageProcessorFactory messageProcessorFactory)
        {
            _messageProcessorFactoryBase = messageProcessorFactory;
        }

        /// <summary>
        /// Gets the necessary telegram bot.
        /// </summary>
        /// <param name="token">The ID of the bot.</param>
        /// <param name="telegramBot">Type of the necessary telegram bot.</param>
        /// <returns>Necessary telegram bot.</returns>
        public IService GetTelegramBot(string token, TelegramBot telegramBot)
        {
            switch (telegramBot)
            {
                case TelegramBot.CarInfoService:
                    return new CarInfoService(new TelegramBotClient(token),
                        _messageProcessorFactoryBase.GetMessageProcessor());
                case TelegramBot.EnglishService:
                    return new EnglishService(new TelegramBotClient(token),
                        _messageProcessorFactoryBase.GetMessageProcessor());
                default:
                    throw new ArgumentException();
            }
        }
    }
}