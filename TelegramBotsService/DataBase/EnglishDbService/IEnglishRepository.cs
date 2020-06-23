using System.Collections.Generic;
using EnglishCommon.Models;

namespace EnglishDbService
{
    /// <summary>
    /// Declares functionality for the repository storing englishExercise
    /// </summary>
    public interface IEnglishRepository
    {
        /// <summary>
        /// Gets all englishExercise from the repository
        /// </summary>
        /// <returns>List of existing users</returns>
        IEnumerable<EnglishExercise> GetExercises();

        /// <summary>
        /// Saves englishExercise to DB
        /// </summary>
        /// <param name="englishExercise">EnglishExercise to save</param>
        /// <returns>Saved englishExercise</returns>
        EnglishExercise Save(EnglishExercise englishExercise);

        /// <summary>
        /// Gets englishExercise by id
        /// </summary>
        /// <param name="id">EnglishExercise identifier</param>
        /// <returns>returns the englishExercise by id</returns>
        EnglishExercise GetEnglishExercise(string id);
    }
}
