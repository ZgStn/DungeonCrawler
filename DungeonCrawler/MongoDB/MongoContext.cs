using DungeonCrawler.Elements;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace DungeonCrawler.Collections
{
    public class MongoContext
    {
        public MongoClient _client;
        public IMongoDatabase _database;

        public MongoContext(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName); 
        }


        public void SaveGameState(List<LevelElement> elements)
        {

        }

        public void LoadGameState(List<LevelElement> elements)
        {
          
        }

        public void CreateDatabase()
        {
            var characterCollection = _database.GetCollection<Character>("Characters");

            var collectionExists = _database.ListCollectionNames().ToList().Contains("Characters");

            if (!collectionExists)
            {
                var characters = new List<Character>
              { 
                new Character
                {
                    Name = "Warrior"

                },

                new Character
                {
                    Name = "Explorer"

                },

                new Character
                {
                    Name = "Escaper"

                }

              };
                
                characterCollection.InsertMany(characters);
            }

        }

    }
}
