using Telegram.Bot.Types.ReplyMarkups;

namespace AvtoCarDriveBot.Resources
{
    /// <summary>
    /// Provides menus for users and admins
    /// </summary>
    public static class Menus
    {
        /// <summary>
        /// The admin menu
        /// </summary>
        public static ReplyKeyboardMarkup AdminKeyboard => new ReplyKeyboardMarkup
        {
            Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(Resources.Cars), new KeyboardButton(Resources.AddFeedback)
                },
                new[]
                {
                    new KeyboardButton(Resources.Settings), new KeyboardButton(Resources.UserMenu)
                }
            },

            ResizeKeyboard = true
        };

        /// <summary>
        /// The admin menu
        /// </summary>
        public static ReplyKeyboardMarkup BackKeyboard => new ReplyKeyboardMarkup
        {
            Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(Resources.Back)
                }
            },

            ResizeKeyboard = true
        };

        /// <summary>
        /// The settings menu
        /// </summary>
        public static ReplyKeyboardMarkup SettingsKeyboard => new ReplyKeyboardMarkup
        {
            Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(Resources.AddNewAdmin), new KeyboardButton(Resources.RemoveAdmin)
                },
                new[]
                {
                    new KeyboardButton(Resources.ChangeTitle), new KeyboardButton(Resources.ChangeDescription)
                },
                new[]
                {
                    new KeyboardButton(Resources.ListOfAdmins), new KeyboardButton(Resources.Back)
                }
            },

            ResizeKeyboard = true
        };
        
        /// <summary>
        /// The user menu
        /// </summary>
        public static ReplyKeyboardMarkup UserKeyboard => new ReplyKeyboardMarkup
        {
            Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(Resources.StockCars), new KeyboardButton(Resources.TurnkeyAutos)
                },
                new[]
                {
                    new KeyboardButton(Resources.Feedbacks), new KeyboardButton(Resources.Back)
                }
            },

            ResizeKeyboard = true
        };
        
        /// <summary>
        /// The cars menu
        /// </summary>
        public static ReplyKeyboardMarkup CarsKeyboard => new ReplyKeyboardMarkup
        {
            Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(Resources.AddCar), new KeyboardButton(Resources.RemoveCar)
                },
                new[]
                {
                    new KeyboardButton(Resources.ChangeCar), new KeyboardButton(Resources.Back)
                }
            },

            ResizeKeyboard = true
        };

        /// <summary>
        /// The admin menu
        /// </summary>
        public static ReplyKeyboardMarkup CarInfoMenu => new ReplyKeyboardMarkup
        {
            Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton(Resources.Done), new KeyboardButton(Resources.Back)
                }
            },

            ResizeKeyboard = true
        };
    }
}