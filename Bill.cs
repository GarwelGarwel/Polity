using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polity
{
    /// <summary>
    /// Possible law that needs Parliament approval
    /// </summary>
    public class Bill
    {
        public string Name { get; set; }

        public Issues Politics { get; set; }

        /// <summary>
        /// % of votes for approval
        /// </summary>
        public double VotesNeeded { get; set; } = 0.50;

        public Effect Effect { get; set; }

        public void Enact() => Effect.Run();

        public Bill() { }

        public Bill(Issues politics, params Effect[] effects)
        {
            Politics = politics;
            Effect = new MultipleEffects(effects);
        }

        //public Bill(Issues politics, Effect effect)
        //{
        //    Politics = politics;
        //    Effect = effect;
        //}
    }
}
