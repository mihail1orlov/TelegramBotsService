using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AvtoCarDriveBot.Services
{
    /// <summary>
    /// Identifies the type of message
    /// </summary>
    public interface IMessageTypeIdentifier
    {
        /// <summary>
        /// Gets the message type
        /// </summary>
        /// <param name="message">Target message</param>
        /// <returns>Type of message</returns>
        MessageType IdentifyMessageType(Message message);
    }
}