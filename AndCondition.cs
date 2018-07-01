using System.Collections.Generic;

namespace Polity
{
    /// <summary>
    /// True when all sub-conditions are true
    /// </summary>
    public class AndCondition : Condition
    {
        public List<Condition> Subconditions { get; set; }

        public void AddSubcondition(Condition c) => Subconditions.Add(c);

        public override string ToString()
        {
            string res = "AND (";
            bool firstItem = true;
            foreach (Condition c in Subconditions)
            {
                if (firstItem) firstItem = false;
                else res += ", ";
                res += c;
            }
            res += ")";
            return res;
        }

        protected override bool Check()
        {
            foreach (Condition c in Subconditions)
                if (!c) return false;
            return true;
        }

        public AndCondition() { }

        public AndCondition(List<Condition> conditions) => Subconditions = conditions;

        public AndCondition(params Condition[] conditions)
        {
            foreach (Condition c in conditions) AddSubcondition(c);
        }
    }
}
