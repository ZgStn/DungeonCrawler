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

            var levelData = mongoContext.LoadLevelData();

            if (levelData is null)
            {
                levelData = new LevelData();
                levelData.Load("Level1.txt");
                await userConfiguration.NewGameStartAsync(levelData);
            }
            else
            {
                int result = userConfiguration.LoadGameOrNewGameMenu();
                Console.Clear();

                if (result == 2)
                {
                    levelData = new LevelData();
                    levelData.Load("Level1.txt");
                    await userConfiguration.NewGameStartAsync(levelData);
                }
            }
            
            GameLoop game = new GameLoop(levelData, mongoContext);
            await game.RunAsync();
        }
    }
};

