using MongoDbCommon;

namespace CarInfoTelegramBotDbService
{
    /// <summary>
    /// Describes required fields for the connection to the mongo DB
    /// </summary>
    public class MongoDbConnectionSettings : IConnectionSettings
    {
        // todo: need to read data from configuration file
        /// <summary>
        /// Gets host of database from configure file
        /// </summary>
        public string Host => "localhost";

        /// <summary>
        /// Gets port of database from configure file
        /// </summary>
        public int Port => int.Parse("27017");

        /// <summary>
        /// Gets database name
        /// </summary>
        public string Database => "carInfo_db";
    }
}