using System;

namespace CarInfoTelegramBot.Services
{
    public class Receiver : IReceiver
    {
        public void Message()
        {
            Console.WriteLine("Receive the message!");
        }
    }
}