using DungeonCrawler.MongoDB;

namespace DungeonCrawler
{

    class Program
    {
        static void Main(string[] args)
        {
            var mongoContext = new MongoContext("mongodb://localhost:27017/", "OzgeStenstrom");

            mongoContext.CreateDatabase();



            Console.CursorVisible = false;

            LevelData levelData = new LevelData();


            levelData.Load("Level1.txt");
            mongoContext.SaveGameState(levelData);


            GameLoop game = new GameLoop(levelData);
            game.Run();
        }
    }
};

