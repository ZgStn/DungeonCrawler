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
      
        public void SaveLevelData(LevelData leveldata) // TODO: make async 
        {
            var saveGameCollection = _database.GetCollection<LevelData>("SavedGame");
            saveGameCollection.ReplaceOne(
            filter: FilterDefinition<LevelData>.Empty, 
            replacement: leveldata,                   
            options: new ReplaceOptions
            {
                IsUpsert = true                       
            });
        }

        public async Task DeleteLevelDataAsync(LevelData levelData)
        {
            var saveGameCollection = _database.GetCollection<LevelData>("SavedGame");

            await saveGameCollection.DeleteOneAsync(filter: FilterDefinition<LevelData>.Empty); // TODO: kan vara deleteMany-säkrare att ha many
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
