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



            var levelData = mongoContext.LoadLevelData();
            if (levelData == null)
            {
                levelData = new LevelData();
                levelData.Load("Level1.txt");
            }
            else
            {
                //TODO: skriv det synggare senare
                levelData.Player = (Player)levelData.Elements.First(l => l.GetType() == typeof(Player));
            }

            //TODO: skapa method(?) här att välja character


            mongoContext.SaveLevelData(levelData);




            GameLoop game = new GameLoop(levelData, mongoContext);
            game.Run();
        }
    }
};

