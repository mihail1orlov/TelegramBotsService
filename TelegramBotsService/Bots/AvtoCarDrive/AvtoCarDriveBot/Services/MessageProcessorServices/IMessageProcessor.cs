using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AvtoCarDriveBot.Services.MessageProcessorServices
{
    /// <summary>
    /// Provides processing of received messages 
    /// </summary>
    public interface IMessageProcessor
    {
        /// <summary>
        /// Processes the received message
        /// </summary>
        /// <param name="message">The received message</param>
        /// <param name="currentChat">The chat currentChat</param>
        /// <param name="messageType">The type of current message</param>
        /// <returns>Process message</returns>
        Task Process(Message message, Chat currentChat, MessageType messageType = MessageType.Unknown);
    }
}