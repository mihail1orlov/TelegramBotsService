namespace MongoDbCommon
{
    public interface IConnectionSettings
    {
        /// <summary>
        /// Gets host of database from configure file
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Gets port of database from configure file
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Gets database name
        /// </summary>
        string Database { get; }
    }
}