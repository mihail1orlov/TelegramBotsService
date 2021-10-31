using System.Linq;
using System.Threading.Tasks;
using AvtoCarDriveBot.Services.AdminIdsServices;
using LoggerCommon;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AvtoCarDriveBot.Services.MessageProcessorServices
{
    /// <summary>
    /// Provides processing of received messages
    /// </summary>
    public class MessageProcessor : IMessageProcessor
    {
        // Private fields
        private long _id;
        private readonly IMessageProcessor _adminMessageProcessor;
        private readonly IMessageProcessor _userMessageProcessor;
        private readonly IAdminIdsProvider _adminIdsProvider;
        private readonly IMessageTypeIdentifier _messageTypeIdentifier;
        private long[] _adminIds;

        /// <summary>
        /// Creates instance of <see cref="MessageProcessor"/>
        /// </summary>
        /// <param name="logger">Provides logging</param>
        /// <param name="telegramBotClient">A client interface to use the Telegram Bot API</param>
        /// <param name="messageProcessorFactory">Provides the necessary message processor</param>
        /// <param name="adminIdsProvider">Provides the list of admins</param>
        /// <param name="messageTypeIdentifier">Identifies the type of message</param>
        public MessageProcessor(ILogger logger, ITelegramBotClient telegramBotClient,
            IMessageProcessorFactory messageProcessorFactory, IAdminIdsProvider adminIdsProvider,
            IMessageTypeIdentifier messageTypeIdentifier)
        {
            _adminIdsProvider = adminIdsProvider;
            _messageTypeIdentifier = messageTypeIdentifier;
            _adminMessageProcessor = messageProcessorFactory.GetMessageProcessor<AdminMessageProcessor>();
            _userMessageProcessor = messageProcessorFactory.GetMessageProcessor<UserMessageProcessor>();
            _adminIds = adminIdsProvider.AdminIds;

            adminIdsProvider.AdminListUpdatedAction += UpdateAdminIds;
        }

        /// <summary>
        /// Processes the received message
        /// </summary>
        /// <param name="message">The received message</param>
        /// <param name="currentChat">The current chat object</param>
        /// <param name="messageType">messageType</param>
        /// <returns>Process message</returns>
        // ReSharper disable once RedundantAssignment
        public async Task Process(Message message, Chat currentChat, MessageType messageType)
        {
            _id = currentChat.Id;
            messageType = _messageTypeIdentifier.IdentifyMessageType(message);

            if (_adminIds.Contains(_id))
            {
                await _adminMessageProcessor.Process(message, currentChat, messageType);
            }
            else
            {
                await _userMessageProcessor.Process(message, currentChat, messageType);
            }
        }

        /// <summary>
        /// Updates the admin ids
        /// </summary>
        private void UpdateAdminIds()
        {
            _adminIds = _adminIdsProvider.AdminIds;
        }
    }
}