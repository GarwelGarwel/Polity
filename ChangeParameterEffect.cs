using System;

namespace Polity
{
    /// <summary>
    /// Effect that changes a specific Parameter of a Country
    /// </summary>
    public class ChangeParameterEffect : Effect
    {
        /// <summary>
        /// Which Country, Person, etc. is affected
        /// </summary>
        public ParameterEntity Target { get; set; }

        /// <summary>
        /// Which Parameter to change
        /// </summary>
        public ParameterIds ParameterId { get; set; }

        public Parameter Parameter
        {
            get => Target.Parameters[ParameterId];
            set => Target.Parameters[ParameterId] = value;
        }

        /// <summary>
        /// Value which the Parameter is multiplied by
        /// </summary>
        public double Multiplier { get; set; } = 1;

        /// <summary>
        /// Value, which is added to the Parameter
        /// </summary>
        public double Addend { get; set; }

        public override string Description
        {
            get
            {
                string res = "";
                if (Multiplier != 1)
                {
                    if (Multiplier > 1) res = "increase "; else res = "decrease ";
                    res += Parameter.Name + " by " + Math.Round(Math.Abs(100 * (Multiplier - 1))) + "%";
                }
                if (Addend != 0)
                {
                    if (Multiplier != 1) res += ", ";
                    if (Addend > 0) res += "increase "; else res += "decrease ";
                    res += Parameter.Name + " by " + Addend;
                }
                return res;
            }
        }

        public override void Run()
        {
            base.Run();
            Parameter.Value = Parameter.Value * Multiplier + Addend;
        }

        public ChangeParameterEffect(ParameterEntity target, ParameterIds parameterId)
        {
            Target = target;
            ParameterId = parameterId;
        }

        public ChangeParameterEffect(ParameterEntity target, ParameterIds parameterId, double multiplier, double addend)
        {
            Target = target;
            ParameterId = parameterId;
            Multiplier = multiplier;
            Addend = addend;
        }
    }
}
