using System.Collections.Generic;

namespace AvtoCarDriveBot.Models
{
    /// <summary>
    /// The car model
    /// </summary>
    public class CarModel
    {
        /// <summary>
        /// The photos of the car
        /// </summary>
        public List<string> CarPhotos { get; set; } = new List<string>();

        /// <summary>
        /// The description about the car
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The car id
        /// </summary>
        public long CarId { get; set; }
    }
}