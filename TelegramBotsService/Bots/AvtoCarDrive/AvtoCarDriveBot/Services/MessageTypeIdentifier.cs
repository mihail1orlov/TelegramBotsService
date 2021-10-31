using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AvtoCarDriveBot.Services
{
    /// <summary>
    /// Identifies the type of message
    /// </summary>
    public class MessageTypeIdentifier : IMessageTypeIdentifier
    {
        /// <summary>
        /// Gets the message type
        /// </summary>
        /// <param name="message">Target message</param>
        /// <returns>Type of message</returns>
        public MessageType IdentifyMessageType(Message message)
        {
            if (message.Text != null)
            {
                return MessageType.Text;
            }

            if (message.Photo != null)
            {
                return MessageType.Text;
            }

            if (message.Voice != null)
            {
                return MessageType.Voice;
            }

            if (message.Video != null)
            {
                return MessageType.Video;
            }

            if (message.Video != null)
            {
                return MessageType.Document;
            }

            if (message.Video != null)
            {
                return MessageType.Sticker;
            }

            return MessageType.Unknown;
        }
    }
}