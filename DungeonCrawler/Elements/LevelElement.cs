using MongoDB.Bson.Serialization.Attributes;

namespace DungeonCrawler.Elements
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(Wall), typeof(Player), typeof(Rat), typeof(Snake))]
    public abstract class LevelElement
    {
        public Position Position { get; set; }

        public char Symbol { get; set; }

        public ConsoleColor Color { get; set; }

        public bool HasBeenSeen { get; set; } = false;

        public LevelElement(Position position, char symbol, ConsoleColor color)
        {
            Position = position;
            Symbol = symbol;
            Color = color;
        }

        public void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y + 3);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
            HasBeenSeen = true;
        }
    }
}
