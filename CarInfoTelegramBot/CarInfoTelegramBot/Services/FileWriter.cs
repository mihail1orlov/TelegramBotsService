using System.IO;
using CarInfoCommon.Models;
using Newtonsoft.Json;

namespace CarInfoTelegramBot.Services
{
    public class FileWriter : IRepository
    {
        private readonly string _path = "carInfo.json";

        public void Save(CarInfo carInfo)
        {
            var contents = JsonConvert.SerializeObject(carInfo);
            File.WriteAllText(_path, contents);
        }

        public CarInfo Load()
        {
            var json = File.ReadAllText(_path);
            var carInfo = JsonConvert.DeserializeObject<CarInfo>(json);
            return carInfo;
        }
    }
}