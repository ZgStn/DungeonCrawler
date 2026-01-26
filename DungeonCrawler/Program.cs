using DungeonCrawler.MongoDB;

namespace DungeonCrawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mongoContext = new MongoContext("mongodb://localhost:27017/", "OzgeStenstrom");

            var userConfiguration = new UserConfiguration(mongoContext);

            //var levelData = new LevelData();

            mongoContext.CreateDatabase();

            // TODO: New game or Continue

            //await userConfiguration.SavedOrNewGameAsync(levelData);

            userConfiguration.DisplayIntroText();

            ConsoleKeyInfo pressedKey = Console.ReadKey(true);

            if (pressedKey.Key == ConsoleKey.Enter)
            {

            }

            Console.Clear();
            Console.CursorVisible = true;

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

            GameLoop game = new GameLoop(levelData, mongoContext);
            await game.RunAsync();
        }
    }
};

