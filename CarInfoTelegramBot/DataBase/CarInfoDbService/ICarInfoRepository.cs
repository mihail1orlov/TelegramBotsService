using System.Collections.Generic;
using CarInfoCommon.Models;

namespace CarInfoDbService
{
    /// <summary>
    /// Declares functionality for the repository storing car info
    /// </summary>
    public interface ICarInfoRepository
    {
        /// <summary>
        /// Gets all catInfo from the repository
        /// </summary>
        /// <returns>List of existing users</returns>
        IEnumerable<CarInfo> GetCarInfos();

        /// <summary>
        /// Saves carInfo to DB
        /// </summary>
        /// <param name="carInfo">CarInfo to save</param>
        /// <returns>Saved carInfo</returns>
        CarInfo Save(CarInfo carInfo);

        /// <summary>
        /// Gets carInfo by id
        /// </summary>
        /// <param name="id">CarInfo identifier</param>
        /// <returns>returns the carInfo by id</returns>
        CarInfo GetCarInfo(string id);
    }
}