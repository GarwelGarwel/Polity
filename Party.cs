namespace Polity
{
    /// <summary>
    /// Political party (possibly represented in the Parliament)
    /// </summary>
    public class Party : ParameterEntity
    {
        public string Name { get; set; }

        public Issues Platform { get; set; } = new Issues();

        public override string ToString() => Name;

        public Party() { }

        public Party(string name) => Name = name;
    }
}
