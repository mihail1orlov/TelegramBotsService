using System.IO;
using CarInfoTelegramBot.Model;
using Newtonsoft.Json;

namespace CarInfoTelegramBot
{
    public class FileWriter
    {
        public void Write(CarInfo carInfo)
        {
            var contents = JsonConvert.SerializeObject(carInfo);
            var path = $"{nameof(carInfo)}.json";
            File.WriteAllText(path, contents);
        }
    }
}