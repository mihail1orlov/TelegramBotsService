using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDbCommon
{
    /// <summary>
    /// Declares interface for the repository entity
    /// </summary>
    /// <typeparam name="TEntity">Type of the entities which will be read/write to the repository</typeparam>
    /// <typeparam name="TIdentifier">Type of the identifier for entities which will be read/write to the repository</typeparam>
    public interface IRepository<TEntity, in TIdentifier> where TEntity : class, IDbEntity<TIdentifier>
    {
        /// <summary>
        /// Gets entity from the data base using identifier
        /// </summary>
        /// <param name="id">Identifier of the entity</param>
        /// <returns>The required entity</returns>
        TEntity Get(TIdentifier id);

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>Enumerable of the all entities</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Searches all entities from the repository which are success
        /// </summary>
        /// <returns>Enumerable of the all entities</returns>
        IEnumerable<TEntity> FindAll(string query);

        /// <summary>
        /// Searches entities from the repository which are success expression
        /// </summary>
        /// <param name="expression">Expression for filtering</param>
        /// <returns>Enumerable of the all entities</returns>
        IFindFluent<TEntity, TEntity> Find(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Creates a queryable source of documents.
        /// </summary>
        /// <param name="aggregateOptions">Aggregate Options</param>
        /// <returns>Queryable object</returns>
        IMongoQueryable<TEntity> AsQueryable(AggregateOptions aggregateOptions = null);

        /// <summary>
        /// Saves the entity to the repository
        /// </summary>
        /// <param name="entity">Entity should be saved</param>
        /// <param name="isUpInsert">Value indicating whether to insert the document if it doesn't already exist</param>
        /// <returns>Saved entity</returns>
        TEntity Save(TEntity entity, bool isUpInsert = true);

        /// <summary>
        /// Deletes a single entity.
        /// </summary>
        /// <param name="id">The entity identifier</param>
        void Delete(TIdentifier id);

        /// <summary>
        /// Deletes a single entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(TEntity entity);
    }
}