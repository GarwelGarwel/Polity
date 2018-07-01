using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polity
{
    /// <summary>
    /// User-chosen event
    /// </summary>
    public class Decision : Event
    {
        public Condition DisplayCondition { get; set; }

        public bool CheckDisplay => !(HappensOnce && HasHappened) && ((DisplayCondition == null) || DisplayCondition);

        public override bool Check => CheckDisplay && base.Check;

        public Decision() { }

        public Decision(string name) => Name = name;

        public Decision(Condition displayCondition, Condition condition, Effect effect)
        {
            DisplayCondition = displayCondition;
            Condition = condition;
            Effect = effect;
        }
    }
}
