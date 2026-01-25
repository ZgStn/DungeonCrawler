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

            // TODO: New game or Continue

            Console.CursorVisible = false;
            Console.WriteLine(
       """
                        
        
                              Welcome to The Dungeon!

            Move your character using the Up, Down, Left and Right arrow keys

            If you encounter enemies, you can attack them by colliding with them

                       To create your character, press Enter 
        """);

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

            mongoContext.SaveLevelData(levelData);

            GameLoop game = new GameLoop(levelData, mongoContext);
            game.Run();
        }
    }
};

