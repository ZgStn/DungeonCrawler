using MongoDB.Driver;

namespace DungeonCrawler.MongoDB
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

        //LevelData innehåller LevelElements och player. Det finns load
        public void SaveLevelData(LevelData leveldata)
        {
            var saveGameCollection = _database.GetCollection<LevelData>("SavedGame");


            saveGameCollection.ReplaceOne(
       filter: FilterDefinition<LevelData>.Empty, // matches "any document"
       replacement: leveldata,                    // replaces whole document
       options: new ReplaceOptions
       {
           IsUpsert = true                        // insert if none exists
       });

            //saveGameCollection.InsertOne(leveldata);
        }

        // det var void- blev LevelData
        public LevelData LoadLevelData()
        {
            var savedGameCollection = _database.GetCollection<LevelData>("SavedGame");
            return savedGameCollection.Find(_ => true).FirstOrDefault();


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
