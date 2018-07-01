namespace Polity
{
    /// <summary>
    /// Some change to the Game or Country or something else that can be caused by a Decision, Event, etc.
    /// </summary>
    public abstract class Effect
    {
        /// <summary>
        /// Human-readable description, e.g. "Increase Income Tax Rate by 0.02" (no full stop in the end)
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Apply effect
        /// </summary>
        public virtual void Run() => Game.TheGame.AppendLog(Description + ".");
    }
}
