using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using AvtoCarDriveBot.Models;

namespace AvtoCarDriveBot.Services.CarServices
{
    /// <summary>
    /// Provides working on the available cars
    /// </summary>
    [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
    public class CarService : ICarService
    {
        // private fields
        private readonly string _dataPath;
        private readonly List<CarModel> _availableCars;
        private event Action CarListUpdatedAction;

        /// <summary>
        /// Creates instance of <see cref="CarService"/>
        /// </summary>
        public CarService(string dataPath = default)
        {
            _dataPath = dataPath ?? Environment.CurrentDirectory + "/Data";

            _availableCars = GetCars();

            CarListUpdatedAction += UpdateCarsFile;
        }

        /// <summary>
        /// Gets the available cars
        /// </summary>
        /// <returns>The available cars</returns>
        public IEnumerable<CarModel> AvailableCars => _availableCars;

        /// <summary>
        /// Adds thew new car to the list of available cars
        /// </summary>
        /// <param name="carModel">The model of the current car</param>
        /// <returns>True if the car is successfully added, otherwise - false</returns>
        public void AddNewCar(CarModel carModel)
        {
            _availableCars.Add(carModel);
            CarListUpdatedAction?.Invoke();
        }

        /// <summary>
        /// Removes the car from the list of available cars
        /// </summary>
        /// <param name="carId">The car id to delete</param>
        /// <returns>True if the car is successfully added, otherwise - false</returns>
        public bool RemoveCar(int carId)
        {
            var car = _availableCars.FirstOrDefault(carModel => carModel.CarId == carId);
            if (car == null)
            {
                return false;
            }

            _availableCars.Remove(car);
            CarListUpdatedAction?.Invoke();
            return true;
        }

        /// <summary>
        /// Changes the car from the list of available cars
        /// </summary>
        /// <param name="carId">The car id for change</param>
        /// <returns></returns>
        public bool ChangeCar(int carId)
        {
            return true;
        }

        /// <summary>
        /// Gets the list of cars from the file
        /// </summary>
        /// <returns>The list of cars from the file</returns>
        private List<CarModel> GetCars()
        {
            var pathApp = _dataPath + "/Cars.txt";
            var cars = new List<CarModel>();
            Directory.CreateDirectory(_dataPath);
            using (var fileStream = new FileStream(pathApp, FileMode.OpenOrCreate, FileAccess.Read))
            {
                var buffer = new byte[100000];
                var bufferSize = fileStream.Read(buffer, 0, 1000);
                if (bufferSize == 0)
                {
                    return cars;
                }

                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(buffer, 0, bufferSize);
                    memoryStream.Position = 0;
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, true))
                    using (var binaryReader = new BinaryReader(deflateStream))
                    {
                        var carCount = binaryReader.ReadInt32();

                        for (var i = 0; i < carCount; i++)
                        {
                            var car = new CarModel();
                            var carItemCount = binaryReader.ReadInt32();
                            for (var j = 2; j < carItemCount; j++)
                            {
                                car.CarPhotos.Add(binaryReader.ReadString());
                            }

                            car.Description = binaryReader.ReadString();
                            car.CarId = binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            cars.Add(car);
                        }
                    }
                }
            }

            return cars;
        }

        /// <summary>
        /// Updates the list of cars file
        /// </summary>
        private void UpdateCarsFile()
        {
            var pathApp = _dataPath + "/Cars.txt";
            Directory.CreateDirectory(_dataPath);

            using (var fileStream = new FileStream(pathApp, FileMode.OpenOrCreate, FileAccess.Write))
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
                using (var binaryWriter = new BinaryWriter(deflateStream))
                {
                    binaryWriter.Write(_availableCars.Count);

                    foreach (var availableCar in _availableCars)
                    {
                        var carItemsCount = availableCar.CarPhotos.Count + 2;
                        binaryWriter.Write(carItemsCount);

                        foreach (var photo in availableCar.CarPhotos)
                        {
                            binaryWriter.Write(photo);
                        }

                        binaryWriter.Write(availableCar.Description);
                        binaryWriter.Write(availableCar.CarId);
                    }
                }

                var buffer = memoryStream.GetBuffer();
                fileStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}