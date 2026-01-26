using DungeonCrawler.MongoDB;

namespace DungeonCrawler
{
    internal class UserConfiguration
    {
        private readonly MongoContext _mongoContext;

        public UserConfiguration(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task NewGameStartAsync(LevelData levelData)
        {
            DisplayIntroText();
            Console.Clear();
            levelData.Player.Name = SelectName();
            Console.Clear();
            levelData.Player.SelectedCharacter = await SelectCharacterAsync();
            Console.Clear();
        }

        public void DisplayIntroText()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(
       """
                        
        
                              Welcome to The Dungeon!
                                
                                 MUAHAHAHAHAHAHA!!!

            Move your character using the Up, Down, Left and Right arrow keys

            If you encounter enemies, you can attack them by colliding with them
                                    
                       To create your character, press Enter

                            To exit program, press Esc
        """);
            
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);

            if (pressedKey.Key == ConsoleKey.Enter)
            {

            }

            Console.ResetColor();
        }

        public int LoadGameOrNewGameMenu()
        {
            Console.WriteLine("1. Continue game\n2. New game");

            while (true)
            {
                ConsoleKeyInfo playerInput = Console.ReadKey(true);

                if (playerInput.Key == ConsoleKey.D1)
                {
                    return 1;

                }
                else if (playerInput.Key == ConsoleKey.D2)
                {
                    return 2;
                }
            }
        }

        public string SelectName()
        {
            Console.CursorVisible = true;

            Console.WriteLine("Choose player name: ");
            return Console.ReadLine();
        }

        public async Task<Character> SelectCharacterAsync()
        {
            Console.WriteLine();
            Console.WriteLine("What do you want to play as?\n1. Explorer\n2. Warrior\n3. Escaper");

            while (true)
            {
                ConsoleKeyInfo playerInput = Console.ReadKey(true);

                if (playerInput.Key == ConsoleKey.D1)
                    return await _mongoContext.GetCharacterByNameAsync("Explorer");
                else if (playerInput.Key == ConsoleKey.D2)
                    return await _mongoContext.GetCharacterByNameAsync("Warrior");
                else if (playerInput.Key == ConsoleKey.D3)
                    return await _mongoContext.GetCharacterByNameAsync("Escaper");
            }
        }
    }
}
