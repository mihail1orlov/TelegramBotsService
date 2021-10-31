using System.Collections.Generic;
using System.Threading.Tasks;
using AvtoCarDriveBot.Resources;
using AvtoCarDriveBot.Services.CarServices;
using LoggerCommon;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AvtoCarDriveBot.Services.MessageProcessorServices
{
    public class UserMessageProcessor : MessageProcessorBase
    {
        // Private fields
        private ReplyKeyboardMarkup _currentMenu;
        private readonly List<ReplyKeyboardMarkup> _previousMenus = new List<ReplyKeyboardMarkup>();
        private readonly ICarService _carService;
        private bool _messageFromUserExpected;
        private string _expectedAct;

        /// <summary>
        /// Creates instance of <see cref="MessageProcessor"/>
        /// </summary>
        /// <param name="logger">Provides logging</param>
        /// <param name="telegramBotClient">A client interface to use the Telegram Bot API</param>
        /// <param name="carService">Provides working on the available cars</param>
        public UserMessageProcessor(ILogger logger, ITelegramBotClient telegramBotClient, ICarService carService) :
            base(logger, telegramBotClient)
        {
            _carService = carService;
        }

        /// <summary>
        /// Processes the received message
        /// </summary>
        /// <param name="message">The received message</param>
        /// <param name="currentChat">The current chat object</param>
        /// <param name="messageType"></param>
        /// <returns>Process message</returns>
        public override async Task Process(Message message, Chat currentChat, MessageType messageType)
        {
            UserInit(currentChat);

            if (_messageFromUserExpected && message.Text != Resources.Resources.Back)
            {
                ExpectedMessageProcess(message);
                return;
            }

            switch (messageType)
            {
                case MessageType.Text:
                    TextMessageProcess(message);
                    break;
                default:
                    SendMessage(Resources.Resources.InDevelopment, nameof(Process));
                    break;
            }
        }

        /// <summary>
        /// Processes the expected message from user
        /// </summary>
        /// <param name="message">The current message</param>
        private void ExpectedMessageProcess(Message message)
        {
            switch (_expectedAct)
            {
                default:
                    BackToPreviousMenu();
                    break;
            }
        }

        /// <summary>
        /// Processes the text message
        /// </summary>
        /// <param name="message">The received message</param>
        private void TextMessageProcess(Message message)
        {
            switch (message.Text)
            {
                case "start":
                case "/start":
                    SendMessage(Resources.Resources.StartUserMessage, nameof(TextMessageProcess), Images.GetImages[0], Menus.UserKeyboard);
                    break;

                // Start menu
                case "Авто в наличии":
                    SendListOfAvailableCars();
                    break;
                case "Авто под ключ":
                    SendMessage(Resources.Resources.InDevelopment, nameof(TextMessageProcess));
                    break;
                case "Отзывы":
                    SendMessage(Resources.Resources.InDevelopment, nameof(TextMessageProcess));
                    break;

                case "↩ Назад":
                    BackToPreviousMenu();
                    break;
                default:
                    SendMessage(Resources.Resources.InDevelopment, nameof(TextMessageProcess));
                    break;
            }
        }

        /// <summary>
        /// Sends the list of available cars
        /// </summary>
        private async void SendListOfAvailableCars()
        {
            var cars = _carService.AvailableCars;

            foreach (var carModel in cars)
            {
                SendMessage(nameof(SendListOfAvailableCars), carModel.CarPhotos,
                    carModel.Description + "\nИдентификатор автомобиля : " + carModel.CarId);
                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// Backs to previous menu
        /// </summary>
        private void BackToPreviousMenu()
        {
            if (_previousMenus.Count < 1)
            {
                SendMessage(Resources.Resources.NowhereBack, nameof(BackToPreviousMenu));

                return;
            }

            _messageFromUserExpected = false;
            _expectedAct = default;
            _currentMenu = _previousMenus[^1];
            SendMessage(Resources.Resources.BackMessage, nameof(BackToPreviousMenu), _currentMenu);

            _previousMenus.Remove(_currentMenu);
        }
    }
}