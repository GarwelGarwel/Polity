namespace Polity
{
    /// <summary>
    /// Used for Events and Decisions
    /// </summary>
    public abstract class Condition
    {
        public bool IsInverse { get; set; } = false;

        static public implicit operator bool(Condition c) => (c == null) || (c.Check() ^ c.IsInverse);

        /// <summary>
        /// Checks if the Condition is true
        /// </summary>
        /// <returns></returns>
        abstract protected bool Check();

        public override string ToString() => IsInverse ? "not " : "";
    }
}
