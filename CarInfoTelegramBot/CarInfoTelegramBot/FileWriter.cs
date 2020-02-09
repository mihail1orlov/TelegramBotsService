using System.IO;
using CarInfoTelegramBot.Model;

namespace CarInfoTelegramBot
{
    public class FileWriter
    {
        public void Write(CarInfo carInfo)
        {
            var contents = "MyJson";
            File.WriteAllText($"{nameof(carInfo)}.json", contents);
        }
    }
}