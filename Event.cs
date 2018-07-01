using System.Collections.Generic;

namespace Polity
{
    /// <summary>
    /// If Conditions are met, Effects occur
    /// </summary>
    public class Event
    {
        string description = "";

        public string Name { get; set; } = "";

        public string Description
        {
            get => (description == "") ? Name : Effect.Description;
            set => description = value;
        }

        public Condition Condition { get; set; }

        public bool HappensOnce { get; set; } = false;

        public bool HasHappened { get; set; } = false;

        public Effect Effect { get; set; }

        public void AddEffect(Effect e)
        {
            if (Effect == null)
            {
                Effect = e;
                return;
            }
            if (! (Effect is MultipleEffects)) Effect = new MultipleEffects(Effect);
            ((MultipleEffects)Effect).AddSubeffect(e);
        }

        public virtual bool Check => !(HappensOnce && HasHappened) && (Condition ?? true);
        //{
        //    get
        //    {
        //        if (HappensOnce && HasHappened) return false;
        //        if (Condition == null) return true;
        //        return Condition;
        //    }
        //}

        /// <summary>
        /// Mandatory execution of all Effects, regardless of Conditions. Normally, you should use CheckAndRun()
        /// </summary>
        public void Run()
        {
            if (Description == "") Game.TheGame.AppendLog(Name + " happened!");
            else Game.TheGame.AppendLog(Description);
            Effect.Run();
            HasHappened = true;
        }

        public virtual void CheckAndRun()
        {
            if (Check) Run();
        }

        public Event() { }

        public Event(string name) => Name = name;

        public Event(Condition condition, Effect effect)
        {
            Condition = condition;
            Effect = effect;
        }

        public Event(Effect effect) => Effect = effect;

        public Event(List<Condition> conditions, List<Effect> effects)
        {
            Condition = new AndCondition(conditions);
            Effect = new MultipleEffects(effects);
        }
    }
}
