using System.Collections.Generic;
using CarInfoCommon.Models;
using MongoDB.Driver;
using MongoDbCommon;

namespace CarInfoDbService
{
    /// <summary>
    /// Describes repository for carInfo
    /// </summary>
    public class CarInfoRepository : MongoRepository<CarInfo, string>, ICarInfoRepository
    {
        /// <summary>
        /// Initializes instance of the carInfo repository
        /// </summary>
        /// <param name="client">Mongo client</param>
        /// <param name="connectionSettings">Connections settings</param>
        public CarInfoRepository(IMongoClient client, IConnectionSettings connectionSettings)
            : base(client, connectionSettings, "carInfo_data")
        {
        }

        /// <summary>
        /// Gets all carInfo from the repository
        /// </summary>
        /// <returns>List of existing carInfo</returns>
        public IEnumerable<CarInfo> GetCarInfos()
        {
            return GetAll();
        }

        /// <summary>
        /// Saves carInfo to DB
        /// </summary>
        /// <param name="carInfo">CarInfo to save</param>
        /// <returns>Saved carInfo</returns>
        public CarInfo Save(CarInfo carInfo)
        {
            return base.Save(carInfo);
        }

        /// <summary>
        /// Gets carInfo by id
        /// </summary>
        /// <param name="id">CarInfo identifier</param>
        /// <returns>returns the carInfo by id</returns>
        public CarInfo GetCarInfo(string id)
        {
            return Get(id);
        }
    }
}