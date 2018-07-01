namespace Polity
{
    /// <summary>
    /// Sets, clears or switches a specified Trigger of a specified ParameterEntity (Game.TheGame by default)
    /// </summary>
    public class TriggerEffect : Effect
    {
        ParameterEntity entity;

        public enum Operations { Set, Clear, Switch };

        public ParameterEntity Entity
        {
            get => entity ?? Game.TheGame;
            set => entity = value;
        }

        public string Trigger { get; set; }

        public Operations Operation { get; set; }

        public override string Description
        {
            get
            {
                string res = "";
                switch (Operation)
                {
                    case Operations.Set: res = "Set "; break;
                    case Operations.Clear: res = "Clear "; break;
                    case Operations.Switch: res = "Switch "; break;
                }
                res += Trigger + " trigger";
                return res;
            }
        }

        public override void Run()
        {
            base.Run();
            switch (Operation)
            {
                case Operations.Set: Entity.SetTrigger(Trigger); break;
                case Operations.Clear: Entity.ClearTrigger(Trigger); break;
                case Operations.Switch: if (Entity.IsTriggerSet(Trigger)) Entity.ClearTrigger(Trigger); else Entity.SetTrigger(Trigger); break;
            }
        }

        public TriggerEffect() { }

        public TriggerEffect(ParameterEntity entity, string trigger, Operations operation)
        {
            Entity = entity;
            Trigger = trigger;
            Operation = operation;
        }

        public TriggerEffect(string trigger, Operations operation)
        {
            Trigger = trigger;
            Operation = operation;
        }
    }
}
