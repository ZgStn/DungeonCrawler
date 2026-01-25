using DungeonCrawler.Elements;

namespace DungeonCrawler
{
    public class Player : LivingElement
    {
        public string SelectedCharacter { get; set; }
        public int VisionRange { get; private set; } = 5;

        public Player(Position position)
            : base(position, '@', ConsoleColor.Blue)
        {
            Name = "Özge";
            HP = 100;
            AttackDice = new Dice(2, 6, 2);
            DefenceDice = new Dice(2, 6, 0);
        }

        public override bool ShouldAttack(LivingElement target)
        {
            return target is Enemy;
        }
    }
}
