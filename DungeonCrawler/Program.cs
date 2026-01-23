
using MongoDB.Driver;



namespace DungeonCrawler
{

    class Program
    {
        static void Main(string[] args)

        {
            var connectionString = "mongodb://localhost:27017/";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("OzgeStenstrom");

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
            var characterCollection = database.GetCollection<Character>("Characters");

            // TODO: characterCollection.InsertMany(characters);// insertmany- fel-



            Console.CursorVisible = false;

            LevelData levelData = new LevelData();
            levelData.Load("Level1.txt");

            GameLoop game = new GameLoop(levelData);
            game.Run();
        }
    }
};

