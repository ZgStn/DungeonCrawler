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

        public async Task<Character> GetCharacterByNameAsync(string character)
        {
            var characterCollection = _database.GetCollection<Character>("Characters");
            var filter = Builders<Character>.Filter.Eq(c => c.Name, character);
            return await characterCollection.Find(filter).SingleOrDefaultAsync();
        }

        //LevelData innehåller LevelElements och player. Det finns load
        // TODO: only save when exiting game?
        public void SaveLevelData(LevelData leveldata) // TODO: make async 
        {
            var saveGameCollection = _database.GetCollection<LevelData>("SavedGame");
            saveGameCollection.ReplaceOne(
            filter: FilterDefinition<LevelData>.Empty, // matches "any document"
            replacement: leveldata,                    // replaces whole document
            options: new ReplaceOptions
            {
                IsUpsert = true                        // insert if none exists
            });
        }

        public async Task DeleteLevelDataAsync(LevelData leveldata)
        {
            var saveGameCollection = _database.GetCollection<LevelData>("SavedGame");

            await saveGameCollection.DeleteOneAsync(filter: FilterDefinition<LevelData>.Empty);
        }
        
        public LevelData LoadLevelData() // TODO: make async
        {
            var savedGameCollection = _database.GetCollection<LevelData>("SavedGame");
            return savedGameCollection.Find(_ => true).FirstOrDefault();
        }

        public void CreateDatabase() // TODO: make async
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
