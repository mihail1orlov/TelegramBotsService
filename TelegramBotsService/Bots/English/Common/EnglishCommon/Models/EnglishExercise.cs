using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbCommon;

namespace EnglishCommon.Models
{
    public class EnglishExercise : IDbEntity<string>
    {
        private string _id;

        /// <summary>
        /// Gets or sets identifier
        /// </summary>
        [BsonElement("id")]
        public string Id
        {
            get
            {
                if (string.IsNullOrEmpty(_id))
                {
                    _id = Guid.NewGuid().ToString("N").ToUpper();
                }

                return _id;
            }

            set => _id = value;
        }
    }
}