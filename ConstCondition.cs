namespace Polity
{
    /// <summary>
    /// Condition that is always True or False
    /// </summary>
    public class ConstCondition : Condition
    {
        public bool Value { get; set; }

        protected override bool Check() => Value;

        public override string ToString() => base.ToString() + Value;

        public ConstCondition(bool value = true) => Value = value;
    }
}
