using System.Collections.Generic;
using System.Linq;

namespace Polity
{
    /// <summary>
    /// A list of several Effects
    /// </summary>
    public class MultipleEffects : Effect
    {

        public List<Effect> Subeffects { get; set; } = new List<Effect>();

        public void AddSubeffect(Effect e) => Subeffects.Add(e);

        public override string Description
        {
            get 
            {
                string res = "";
                bool firstItem = true;
                foreach (Effect e in Subeffects)
                {
                    if (firstItem) firstItem = false;
                    else res += ", ";
                    res += e.Description;
                }
                return res;
            }
        }

        public override void Run()
        {
            base.Run();
            foreach (Effect e in Subeffects)
                e.Run();
        }

        public MultipleEffects() { }

        public MultipleEffects(List<Effect> effects) => Subeffects = effects;

        public MultipleEffects(Effect[] effects) => Subeffects = effects.ToList<Effect>();

        public MultipleEffects(Effect effect) => AddSubeffect(effect);
    }
}
