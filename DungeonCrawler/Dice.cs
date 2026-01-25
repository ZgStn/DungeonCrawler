namespace DungeonCrawler
{
    public class Dice
    {
        public int NumberOfDice { get; set; }
        public int SidesPerDie { get; set; }
        public int Modifier { get; set; }

        private static Random random = new Random();

        public Dice(int numberOfDice, int sidesPerDie, int modifier)
        {
            this.NumberOfDice = numberOfDice;
            this.SidesPerDie = sidesPerDie;
            this.Modifier = modifier;
        }

        public int Throw()
        {
            int total = 0;

            for (int i = 0; i < NumberOfDice; i++)
            {
                total += random.Next(1, SidesPerDie + 1);
            }

            return total + Modifier;
        }


        public override string ToString()
        {
            return $"{NumberOfDice}d{SidesPerDie}+{Modifier}";
        }
    }
}
