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

            // TODO: intro text? 

            var levelData = mongoContext.LoadLevelData();
            if (levelData == null)
            {
                levelData = new LevelData();
                levelData.Load("Level1.txt");
            }

            //levelData.Player.SelectedCharacter =
            //TODO: create way for user to choose character

            mongoContext.SaveLevelData(levelData);

            GameLoop game = new GameLoop(levelData, mongoContext);
            game.Run();
        }
    }
};

