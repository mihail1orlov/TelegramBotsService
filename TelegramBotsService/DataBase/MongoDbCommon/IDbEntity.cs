namespace MongoDbCommon
{
    /// <summary>
    /// The interface for an entity which is writable to database
    /// </summary>
    /// <typeparam name="TIdentifier">Identifier for the entity in the database.</typeparam>
    public interface IDbEntity<TIdentifier>
    {
        /// <summary>
        /// Gets or sets identifier for the entity in the database.
        /// </summary>
        TIdentifier Id { get; set; }
    }
}