using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDbCommon
{
    /// <summary>
    /// Describes operations are allowed for mongo DB
    /// </summary>
    /// <typeparam name="TEntity">Type of the entities are manipulated.</typeparam>
    /// <typeparam name="TIdentifier">Type of the identifier for the entities are manipulated.</typeparam>
    public abstract class MongoRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier> where TEntity : class, IDbEntity<TIdentifier>
    {
        #region Private fields

        /// <summary>
        /// Stores collection name
        /// </summary>
        private readonly string _collectionName;

        #endregion

        #region Protected fields

        /// <summary>
        /// Stores DB collection
        /// </summary>
        protected IMongoCollection<TEntity> Collection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes new instance of the class.
        /// </summary>
        /// <param name="client">The client interface to MongoDB.</param>
        /// <param name="connectionSettings">Settings for connections to DB</param>
        /// <param name="collectionName">The collection name</param>
        protected MongoRepository(IMongoClient client, IConnectionSettings connectionSettings, string collectionName)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (connectionSettings == null)
            {
                throw new ArgumentNullException(nameof(connectionSettings));
            }

            _collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));

            var database = client.GetDatabase(connectionSettings.Database);
            Collection = database.GetCollection<TEntity>(_collectionName);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets entity from the data base using identifier
        /// </summary>
        /// <param name="id">The identifier of the entity</param>
        /// <returns>The required entity</returns>
        public TEntity Get(TIdentifier id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync().Result;
        }

        /// <summary>
        /// Gets all entities from the repository
        /// </summary>
        /// <returns>Enumerable of the all entities</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return Collection.Find(new BsonDocument()).ToListAsync().Result;
        }

        /// <summary>
        /// Searches all entities from the repository which are success
        /// </summary>
        /// <returns>Enumerable of the all entities</returns>
        public IEnumerable<TEntity> FindAll(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException(nameof(query));
            }
            FilterDefinition<TEntity> filter = query;
            var listAsync = Collection.Find(filter).ToListAsync();

            return listAsync.Result;
        }

        /// <summary>
        /// Searches all entities from the repository which are success expression
        /// </summary>
        /// <param name="filter">Expression for filtering</param>
        /// <returns>Enumerable of the all entities</returns>
        public IEnumerable<TEntity> FindAll<T>(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }
            var listAsync = Collection.Find(filter).ToListAsync();

            return listAsync.Result;
        }

        /// <summary>
        /// Searches entities from the repository which are success expression
        /// </summary>
        /// <param name="filter">Expression for filtering</param>
        /// <returns>Enumerable of the all entities</returns>
        public IFindFluent<TEntity, TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var ret = Collection.Find(expression);

            return ret;
        }

        /// <summary>
        /// Creates a queryable source of documents.
        /// </summary>
        /// <param name="aggregateOptions">Aggregate Options</param>
        /// <returns>Queryable object</returns>
        public IMongoQueryable<TEntity> AsQueryable(AggregateOptions aggregateOptions = null)
        {
            return Collection.AsQueryable(aggregateOptions);
        }

        /// <summary>
        /// Saves the entity to the repository
        /// </summary>
        /// <param name="entity">Entity should be saved</param>
        /// <param name="isUpInsert">Value indicating whether to insert the document if it doesn't already exist</param>
        /// <returns>Saved entity</returns>
        public TEntity Save(TEntity entity, bool isUpInsert = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Collection
                .ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity, new UpdateOptions { IsUpsert = isUpInsert })
                .Wait();

            return entity;
        }

        /// <summary>
        /// Deletes a single entity.
        /// </summary>
        /// <param name="id">The entity identifier</param>
        public void Delete(TIdentifier id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            Collection.DeleteOneAsync(x => x.Id.Equals(id))
                .Wait();
        }

        /// <summary>
        /// Deletes a single entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Delete(entity.Id);
        }

        #endregion
    }
}