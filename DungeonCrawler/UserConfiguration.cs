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

        public string SelectName()
        {
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
