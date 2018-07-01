namespace Polity
{
    /// <summary>
    /// True when a specified Trigger is set/not set in a specified ParameterEntity (or Game.TheGame by default)
    /// </summary>
    public class TriggerCondition : Condition
    {
        ParameterEntity entity;
        public ParameterEntity Entity
        {
            get => (entity != null) ? entity : Game.TheGame;
            set => entity = value;
        }

        public string Trigger { get; set; }

        public bool Value { get; set; }

        public override string ToString() => Trigger + " is " + (Value ? "" : "not ") + "set";

        protected override bool Check() => Entity.IsTriggerSet(Trigger) == Value;

        public TriggerCondition() { }

        public TriggerCondition(ParameterEntity entity, string trigger, bool value = true)
        {
            Entity = entity;
            Trigger = trigger;
            Value = value;
        }

        public TriggerCondition(string trigger, bool value = true)
        {
            Trigger = trigger;
            Value = value;
        }
    }
}
