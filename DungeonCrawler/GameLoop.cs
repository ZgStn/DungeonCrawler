using DungeonCrawler.Elements;
using DungeonCrawler.MongoDB;

namespace DungeonCrawler
{
    public class GameLoop
    {
        private LevelData levelData;
        private bool isRunning = true;
        private int turnCount = 1;

        private MongoContext mongoContext;

        public GameLoop(LevelData levelData, MongoContext mongoContext)
        {
            this.levelData = levelData;
            this.mongoContext = mongoContext;
        }

        public async Task RunAsync()
        {
            levelData.Draw(levelData.Player, turnCount++);

            while (isRunning)
            {
                await LoopAsync();
            }

            mongoContext.SaveLevelData(levelData);
        }

        public async Task GameOverAsync(LevelData leveldata)
        {
            Console.SetCursorPosition(0, 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have run out of health points and died! Game Over... Press enter to exit");
            Console.ResetColor();
            await mongoContext.DeleteLevelDataAsync(leveldata);
           
            while (true)
            {
                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Environment.Exit(0);
                }
            }
        }

        private async Task LoopAsync()
        {
            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                isRunning = false;
                return;
            }

            Console.Clear();
            Console.ResetColor();
            Console.CursorVisible = false;

            MovePlayer(keyInfo);

            foreach (var element in levelData.Elements)
            {
                if (element is Enemy enemy)
                {
                    enemy.Update(levelData.Player, levelData);
                }
            }

            levelData.Elements.RemoveAll(e => e is LivingElement le && !le.IsAlive && le is not Player);

            levelData.Draw(levelData.Player, turnCount++);
            
            if (!levelData.Player.IsAlive)
            {
                await GameOverAsync(levelData);
            }
        }

        private void MovePlayer(ConsoleKeyInfo keyInfo)
        {
            int movementX = 0;
            int movementY = 0;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    movementY = -1;
                    break;
                case ConsoleKey.DownArrow:
                    movementY = 1;
                    break;
                case ConsoleKey.LeftArrow:
                    movementX = -1;
                    break;
                case ConsoleKey.RightArrow:
                    movementX = 1;
                    break;
                default:
                    break;
            }

            if (movementX != 0 || movementY != 0)
            {
                levelData.Player.Move(movementX, movementY, levelData);
            }
        }
    }
}
