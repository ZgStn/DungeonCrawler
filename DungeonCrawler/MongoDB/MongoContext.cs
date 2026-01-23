using DungeonCrawler.Elements;
using MongoDB.Driver;

namespace DungeonCrawler.Collections
{
    public class MongoContext
    {
        MongoClient _client;
        IMongoDatabase _database;

        public MongoContext(string connectionString, string databaseName)
        {
            //var connectionString = "mongodb://localhost:27017/";
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        public void SaveGameState(List<LevelElement> elements)
        {


        }

        public void LoadGameState(List<LevelElement> elements)
        {
            //    var connectionString = "mongodb://localhost:27017/";
            //    var client = new MongoClient(connectionString);
            //    var database = client.GetDatabase("OzgeStenstrom");


        }


        // save characters ()

        //    var connectionString = "mongodb://localhost:27017/";
        //    var client = new MongoClient(connectionString);
        //    var database = client.GetDatabase("OzgeStenstrom");

        //    var characters = new List<Character>
        //        {
        //            new Character
        //            {
        //                Name = "Warrior"

        //            },

        //            new Character
        //            {
        //                Name = "Explorer"

        //            },

        //            new Character

        //                Name = "Escaper"

        //            }

        //        };
        //var characterCollection = database.GetCollection<Character>("Characters");

        //TODO: characterCollection.InsertMany(characters);// insertmany- fel-


    }
}
