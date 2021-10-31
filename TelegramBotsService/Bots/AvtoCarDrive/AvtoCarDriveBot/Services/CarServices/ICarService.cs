using System.Collections.Generic;
using AvtoCarDriveBot.Models;

namespace AvtoCarDriveBot.Services.CarServices
{
    /// <summary>
    /// Provides working on the available cars
    /// </summary>
    public interface ICarService
    {
        /// <summary>
        /// Gets the available cars
        /// </summary>
        /// <returns>The available cars</returns>
        IEnumerable<CarModel> AvailableCars { get; }

        /// <summary>
        /// Adds thew new car to the list of available cars
        /// </summary>
        /// <param name="carModel">The model of the current car</param>
        /// <returns>True if the car is successfully added, otherwise - false</returns>
        void AddNewCar(CarModel carModel);

        /// <summary>
        /// Removes the car from the list of available cars
        /// </summary>
        /// <param name="carId">The car id to delete</param>
        /// <returns>True if the car is successfully added, otherwise - false</returns>
        bool RemoveCar(int carId);

        /// <summary>
        /// Changes the car from the list of available cars
        /// </summary>
        /// <param name="carId">The car id for change</param>
        /// <returns></returns>
        bool ChangeCar(int carId);
    }
}