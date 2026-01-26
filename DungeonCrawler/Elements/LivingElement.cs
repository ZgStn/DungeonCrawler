namespace DungeonCrawler.Elements
{
    public abstract class LivingElement : LevelElement
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenceDice { get; set; }
        public bool IsAlive { get; set; } = true;

        public LivingElement(Position position, char symbol, ConsoleColor color)
            : base(position, symbol, color)
        {

        }
        public abstract bool ShouldAttack(LivingElement target);

        public void Move(int movementX, int movementY, LevelData levelData)
        {
            var newPosition = new Position(Position.X + movementX, Position.Y + movementY);

            if (!levelData.IsBlocked(newPosition))
            {
                Position = newPosition;
                return;
            }

            var livingElement = levelData.GetLivingElementAt(newPosition);

            if (livingElement != null && ShouldAttack(livingElement))
            {
                Attack(livingElement);
            }

            return;
        }

        public void Attack(LivingElement livingElement)
        {
            // Attack
            var isDefenderAlive = DealDamage(this, livingElement, false);

            if (!isDefenderAlive)
                return;

            // Counter attack
            DealDamage(livingElement, this, true);
        }

        private bool DealDamage(LivingElement attacker, LivingElement defender, bool isCounterAttack)
        {
            int attackRoll = attacker.AttackDice.Throw();
            int defenceRoll = defender.DefenceDice.Throw();
            int damage = attackRoll - defenceRoll;

            if (isCounterAttack)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(0, 2);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, 1);
            }

            Console.Write($"{attacker.Name} (ATK: {attacker.AttackDice} => {attackRoll}) attacked ");
            Console.Write($"{defender.Name} (DEF: {defender.DefenceDice} => {defenceRoll})");

            if (damage > 0)
            {
                defender.HP -= damage;
                Console.Write($" and dealt {damage} damage!");

                if (defender.HP <= 0)
                {
                    Console.Write($" {defender.Name} died!"); 
                    defender.IsAlive = false;
                       return false;
                }
            }
            else
            {
                Console.Write($" but did not manage to make any damage!");
            }

            return true;
        }
    }
}