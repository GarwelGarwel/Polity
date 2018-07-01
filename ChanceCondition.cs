namespace Polity
{
    /// <summary>
    /// Condition is true when a random value 0..1 is less than Chance
    /// </summary>
    class ChanceCondition : Condition
    {
        public double Chance { get; set; }

        protected override bool Check() => Game.R.NextDouble() < Chance;

        public ChanceCondition(double chance) => Chance = chance;
    }
}
