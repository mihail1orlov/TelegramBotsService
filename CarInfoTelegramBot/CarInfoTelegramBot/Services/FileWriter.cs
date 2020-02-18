using System.IO;
using CarInfoCommon.Models;
using Newtonsoft.Json;

namespace CarInfoTelegramBot.Services
{
    public class FileWriter : IRepository
    {
        public void Save(CarInfo carInfo)
        {
            var contents = JsonConvert.SerializeObject(carInfo);
            var path = $"{nameof(carInfo)}.json";
            File.WriteAllText(path, contents);
        }
    }
}