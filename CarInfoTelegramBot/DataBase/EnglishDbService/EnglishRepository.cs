using System.Collections.Generic;
using EnglishCommon.Models;
using MongoDB.Driver;
using MongoDbCommon;

namespace EnglishDbService
{
    /// <summary>
    /// Describes repository for EnglishExercise
    /// </summary>
    public class EnglishRepository : MongoRepository<EnglishExercise, string>, IEnglishRepository
    {
        /// <summary>
        /// Initializes instance of the EnglishExercise repository
        /// </summary>
        /// <param name="client">Mongo client</param>
        /// <param name="connectionSettings">Connections settings</param>
        public EnglishRepository(IMongoClient client, IConnectionSettings connectionSettings)
            : base(client, connectionSettings, "English_data")
        {
        }

        /// <summary>
        /// Gets all EnglishExercise from the repository
        /// </summary>
        /// <returns>List of existing EnglishExercise</returns>
        public IEnumerable<EnglishExercise> GetExercises()
        {
            return GetAll();
        }

        /// <summary>
        /// Saves EnglishExercise to DB
        /// </summary>
        /// <param name="englishExercise">EnglishExercise to save</param>
        /// <returns>Saved carInfo</returns>
        public EnglishExercise Save(EnglishExercise englishExercise)
        {
            return base.Save(englishExercise);
        }

        /// <summary>
        /// Gets englishExercise by id
        /// </summary>
        /// <param name="id"> EnglishExercise identifier</param>
        /// <returns>returns the englishExercise by id</returns>
        public EnglishExercise GetEnglishExercise(string id)
        {
            return Get(id);
        }
    }
}