using System.Collections.Generic;

namespace Polity
{
    public enum IssueIds { Populism, BigGovernment };
    /// <summary>
    /// Represents a set of political positions or considerations
    /// </summary>
    public class Issues
    {
        public Dictionary<IssueIds, double> List { get; set; } = new Dictionary<IssueIds, double>();

        public double this[IssueIds id]
        {
            get => List[id];
            set => List[id] = value;
        }

        public void AddIssue(IssueIds id, double value)
        {
            if (!List.ContainsKey(id))
                List.Add(id, value);
        }

        public static double operator *(Issues i1, Issues i2)
        {
            double res = 0;
            foreach (KeyValuePair<IssueIds, double> kvp in i1.List)
                if (i2.List.ContainsKey(kvp.Key))
                    res += kvp.Value * i2[kvp.Key];
            return res;
        }

        public Issues() { }

        public Issues(IssueIds id, double value) => AddIssue(id, value);
    }
}
