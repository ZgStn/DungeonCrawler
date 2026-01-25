using DungeonCrawler.MongoDB;

namespace DungeonCrawler
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var mongoContext = new MongoContext("mongodb://localhost:27017/", "OzgeStenstrom");

            var userConfiguration = new UserConfiguration(mongoContext);

            mongoContext.CreateDatabase();
            

            // TODO: intro text? 

            var levelData = mongoContext.LoadLevelData();
            if (levelData == null)
            {
                levelData = new LevelData();
                levelData.Load("Level1.txt");
            }

            levelData.Player.Name = userConfiguration.SelectName();
            levelData.Player.SelectedCharacter = await userConfiguration.SelectCharacterAsync(); 
            Console.Clear();
            Console.CursorVisible = false;
            //levelData.Player.SelectedCharacter =
            //TODO: create way for user to choose character

            mongoContext.SaveLevelData(levelData);

            GameLoop game = new GameLoop(levelData, mongoContext);
            game.Run();
        }
    }
};

