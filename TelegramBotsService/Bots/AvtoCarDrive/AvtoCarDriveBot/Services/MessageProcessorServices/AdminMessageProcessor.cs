using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvtoCarDriveBot.Models;
using AvtoCarDriveBot.Resources;
using AvtoCarDriveBot.Services.AdminIdsServices;
using AvtoCarDriveBot.Services.CarServices;
using LoggerCommon;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AvtoCarDriveBot.Services.MessageProcessorServices
{
    /// <summary>
    /// Provides processing of admin received messages
    /// </summary>
    public class AdminMessageProcessor : MessageProcessorBase
    {
        // Private fields
        private ReplyKeyboardMarkup _currentMenu;
        private readonly List<ReplyKeyboardMarkup> _previousMenus = new List<ReplyKeyboardMarkup>();
        private readonly IAdminIdsService _adminIdsService;
        private readonly ICarService _carService;
        private bool _messageFromUserExpected;
        private string _expectedAct;
        private CarModel _carModel;

        /// <summary>
        /// Creates instance of <see cref="MessageProcessor"/>
        /// </summary>
        /// <param name="logger">Provides logging</param>
        /// <param name="telegramBotClient">A client interface to use the Telegram Bot API</param>
        /// <param name="adminIdsService">Provides processing of the list of admins</param>
        /// <param name="carService">Provides working on the available cars</param>
        public AdminMessageProcessor(ILogger logger, ITelegramBotClient telegramBotClient,
            IAdminIdsService adminIdsService, ICarService carService) : base(logger, telegramBotClient)
        {
            _adminIdsService = adminIdsService;
            _carService = carService;
        }

        /// <summary>
        /// Processes the received message
        /// </summary>
        /// <param name="message">The received message</param>
        /// <param name="currentChat">The current chat object</param>
        /// <param name="messageType">The type of message</param>
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
        /// Processes the text message
        /// </summary>
        /// <param name="message">The received message</param>
        private void TextMessageProcess(Message message)
        {
            switch (message.Text)
            {
                case "start":
                case "/start":
                    SendAdminStartMenu();
                    break;

                // Start menu
                case "Автомобили":
                    SendCarsMenu();
                    break;
                case "Добавить отзыв":
                    SendMessage(Resources.Resources.InDevelopment, nameof(TextMessageProcess));
                    break;
                case "Настройки":
                    SendSettingsMenu();
                    break;
                case "Меню клиента":
                    SendUserStartMenu();
                    break;

                // Cars menu
                case "Добавить автомобиль":
                    SendAddNewCarDescMenu();
                    break;
                case "Убрать автомобиль":
                    SendRemoveCarMenu();
                    break;
                case "Изменить автомобиль":
                    SendMessage(Resources.Resources.InDevelopment, nameof(TextMessageProcess));
                    break;

                // Settings menu
                case "Добавить нового админа":
                    SendAddingNewAdminMenu();
                    break;
                case "Убрать админа":
                    SendRemovingNewAdminMenu();
                    break;
                case "Изменить название бота":
                    SendMessage(Resources.Resources.InDevelopment, nameof(TextMessageProcess));
                    break;
                case "Изменить описание бота":
                    SendMessage(Resources.Resources.InDevelopment, nameof(TextMessageProcess));
                    break;
                case "Список админов":
                    SendListOfAdmins();
                    break;

                case "↩ Назад":
                    BackToPreviousMenu();
                    break;
                default:
                    SendMessage(Resources.Resources.InDevelopment, nameof(TextMessageProcess));
                    break;
            }
        }

        #region MenuProcces

        /// <summary>
        /// Processes the expected message from user
        /// </summary>
        /// <param name="message">The current message</param>
        private void ExpectedMessageProcess(Message message)
        {
            switch (_expectedAct)
            {
                case "SendAddingNewAdminMenu":
                    AddNewAdmin(message.Text);
                    break;
                case "SendRemovingNewAdminMenu":
                    RemoveAdmin(message.Text);
                    break;
                case "EnterNewCarInfo":
                    EnterCarInfo(message);
                    break;
                case "SendRemoveCarMenu":
                    RemoveCarInfo(message);
                    break;
                default:
                    BackToPreviousMenu();
                    break;
            }
        }

        /// <summary>
        /// Removes the car from the list of available cars
        /// </summary>
        /// <param name="message"></param>
        private void RemoveCarInfo(Message message)
        {
            if (message.Text != null)
            {
                if (int.TryParse(message.Text, out var carId))
                {
                    SendMessage(_carService.RemoveCar(carId)
                        ? Resources.Resources.CarWasRemoved
                        : Resources.Resources.CarAlreadyRemoved, nameof(EnterCarInfo));
                }
                else
                {
                    SendMessage(Resources.Resources.Error, nameof(EnterCarInfo));
                }
            }

            BackToPreviousMenu();
        }

        /// <summary>
        /// Enters the info about the new car
        /// </summary>
        /// <param name="message">The current message</param>
        private void EnterCarInfo(Message message)
        {
            if (message.Text != null && message.Text == Resources.Resources.Done)
            {
                _carModel.CarId = new Random().Next(100000, 500000);
                _carService.AddNewCar(_carModel);
                _carModel = null;

                SendMessage(Resources.Resources.CarWasAdded, nameof(EnterCarInfo));
                BackToPreviousMenu();
            }

            _carModel ??= new CarModel();

            if (message.Photo != null && message.Photo.Length > 0)
            {
                _carModel.CarPhotos.Add(message.Photo[0].FileId);
            }

            if (message.Text != null)
            {
                _carModel.Description = message.Text;
            }

            SendMessage(Resources.Resources.InfoWasAdded, nameof(EnterCarInfo));
        }

        /// <summary>
        /// Sends the removing car menu for admin
        /// </summary>
        private void SendRemoveCarMenu()
        {
            SendMessage(Resources.Resources.EnterTheIdForRemoving, nameof(SendRemoveCarMenu), Menus.BackKeyboard);
            _messageFromUserExpected = true;
            _expectedAct = "SendRemoveCarMenu";

            _previousMenus.Add(_currentMenu);
            _currentMenu = Menus.BackKeyboard;
        }

        /// <summary>
        /// Sends the adding new car menu for admin
        /// </summary>
        private void SendAddNewCarDescMenu()
        {
            SendMessage(Resources.Resources.EnterCarInfo, nameof(SendAddNewCarDescMenu), Menus.CarInfoMenu);
            _messageFromUserExpected = true;
            _expectedAct = "EnterNewCarInfo";

            _previousMenus.Add(_currentMenu);
            _currentMenu = Menus.CarInfoMenu;
        }

        /// <summary>
        /// Sends the cars menu for admin
        /// </summary>
        private void SendCarsMenu()
        {
            SendMessage(Resources.Resources.StockCars, nameof(SendCarsMenu), Menus.CarsKeyboard);
            SendListOfAvailableCars();

            _previousMenus.Add(_currentMenu);
            _currentMenu = Menus.CarsKeyboard;
        }

        /// <summary>
        /// Sends the list of admins
        /// </summary>
        private void SendListOfAdmins()
        {
            var availableAdmins = string.Format($"{Resources.Resources.AvailableAdmins}\n" +
                                                $"{_adminIdsService}");
            SendMessage(availableAdmins, nameof(SendListOfAdmins), Menus.BackKeyboard);

            _previousMenus.Add(_currentMenu);
            _currentMenu = Menus.BackKeyboard;
        }

        /// <summary>
        /// Adds the new admin
        /// </summary>
        /// <param name="message">The current message</param>
        private void AddNewAdmin(string message)
        {
            if (long.TryParse(message ?? string.Empty, out var newAdminId))
            {
                SendMessage(_adminIdsService.SetAdminId(newAdminId)
                    ? Resources.Resources.AdminWasAdded
                    : Resources.Resources.AdminAlreadyExists, nameof(AddNewAdmin));
            }
            else
            {
                SendMessage(Resources.Resources.Error, nameof(AddNewAdmin));
            }

            BackToPreviousMenu();
        }

        /// <summary>
        /// Removes the admin
        /// </summary>
        /// <param name="message">The current message</param>
        private void RemoveAdmin(string message)
        {
            if (long.TryParse(message ?? string.Empty, out var newAdminId))
            {
                SendMessage(_adminIdsService.RemoveAdminId(newAdminId)
                    ? Resources.Resources.AdminWasRemoved
                    : Resources.Resources.AdminAlreadyRemoved, nameof(RemoveAdmin));
            }
            else
            {
                SendMessage(Resources.Resources.Error, nameof(RemoveAdmin));
            }

            BackToPreviousMenu();
        }

        /// <summary>
        /// Sends the init message for admin
        /// </summary>
        private void SendAdminStartMenu()
        {
            SendMessage(Resources.Resources.StartAdminMessage, nameof(SendAdminStartMenu), Menus.AdminKeyboard);

            _previousMenus.Add(_currentMenu);
            _currentMenu = Menus.AdminKeyboard;
        }

        /// <summary>
        /// Sends the settings menu for admin
        /// </summary>
        private void SendSettingsMenu()
        {
            SendMessage(Resources.Resources.StartAdminMessage, nameof(SendSettingsMenu), Menus.SettingsKeyboard);

            _previousMenus.Add(_currentMenu);
            _currentMenu = Menus.SettingsKeyboard;
        }

        /// <summary>
        /// Sends the init message for user
        /// </summary>
        private void SendUserStartMenu()
        {
            SendMessage(Resources.Resources.StartUserMessage, nameof(SendUserStartMenu), Images.GetImages[0],
                Menus.UserKeyboard);

            _previousMenus.Add(_currentMenu);
            _currentMenu = Menus.UserKeyboard;
        }

        /// <summary>
        /// Sends the message for adding new admin
        /// </summary>
        private void SendAddingNewAdminMenu()
        {
            var availableAdmins = string.Format($"{Resources.Resources.AvailableAdmins}\n" +
                                                $"{_adminIdsService}\n" +
                                                $"{Resources.Resources.EnterTheNewId}:");
            SendMessage(availableAdmins, nameof(SendAddingNewAdminMenu), Menus.BackKeyboard);

            _previousMenus.Add(_currentMenu);
            _currentMenu = Menus.BackKeyboard;
            _messageFromUserExpected = true;
            _expectedAct = "SendAddingNewAdminMenu";
        }

        /// <summary>
        /// Sends the message for removing new admin
        /// </summary>
        private void SendRemovingNewAdminMenu()
        {
            var availableAdmins = string.Format($"{Resources.Resources.AvailableAdmins}\n" +
                                                $"{_adminIdsService}\n" +
                                                $"{Resources.Resources.EnterTheIdForRemoving}:");
            SendMessage(availableAdmins, nameof(SendRemovingNewAdminMenu), Menus.BackKeyboard);

            _previousMenus.Add(_currentMenu);
            _currentMenu = Menus.BackKeyboard;
            _messageFromUserExpected = true;
            _expectedAct = "SendRemovingNewAdminMenu";
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

        #endregion
        
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
    }
}