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



        public void Run()
        {
            levelData.Draw(levelData.Player, turnCount++);

            while (isRunning)
            {
                Loop();
                mongoContext.SaveLevelData(levelData);// lagt till
            }
        }

        private void Loop()
        {
            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                isRunning = false;
                return;
            }

            Console.Clear();
            Console.ResetColor();

            MovePlayer(keyInfo);

            foreach (var element in levelData.Elements)
            {
                if (element is Enemy enemy)
                {
                    enemy.Update(levelData.Player, levelData);
                }
            }

            levelData.Elements.RemoveAll(e => e is LivingElement le && !le.IsAlive);

            levelData.Draw(levelData.Player, turnCount++);
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
