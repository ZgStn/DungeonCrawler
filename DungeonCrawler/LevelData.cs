using DungeonCrawler.Elements;
using MongoDB.Bson.Serialization.Attributes;

namespace DungeonCrawler
{
    [BsonIgnoreExtraElements]
    public class LevelData
    {
        public List<LevelElement> Elements { get; set; } = new List<LevelElement>();

        public Player Player { get { return (Player)Elements.First(element => element is Player); } }
        public void Load(string filename)
        {
            Elements.Clear();

            var lines = File.ReadAllLines(filename);

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];

                    switch (c)
                    {
                        case '#':
                            Elements.Add(new Wall(new Position(x, y)));
                            break;
                        case 'r':
                            Elements.Add(new Rat(new Position(x, y)));
                            break;
                        case 's':
                            Elements.Add(new Snake(new Position(x, y)));
                            break;
                        case '@':
                            Elements.Add(new Player(new Position(x, y)));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public bool IsBlocked(Position position)
        {
            foreach (var levelElement in Elements)
            {
                if (levelElement.Position.X == position.X &&
                    levelElement.Position.Y == position.Y)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(Player player, int turnCount)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{Player.Name} the {Player.SelectedCharacter.Name} (Health: {Player.HP}, Turn: {turnCount})");

            foreach (var levelElement in Elements)
            {
                if (IsVisibleToPlayer(player, levelElement) ||
                    (levelElement is Wall && levelElement.HasBeenSeen))
                {
                    levelElement.Draw();
                }
            }
        }

        private bool IsVisibleToPlayer(Player player, LevelElement element)
        {
            int dx = player.Position.X - element.Position.X;
            int dy = player.Position.Y - element.Position.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            return distance <= player.VisionRange;
        }

        public LivingElement GetLivingElementAt(Position position)
        {
            foreach (var levelElement in Elements)
            {
                if (levelElement.Position.Equals(position) &&
                    levelElement is LivingElement livingElement)
                {
                    return livingElement;
                }
            }

            return null;
        }

        public void Remove(LevelElement element)
        {
            Elements.Remove(element);
        }
    }
}
