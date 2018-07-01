using System.Collections.Generic;

namespace Polity
{
    /// <summary>
    /// Parent class for all classes that have Parameter lists
    /// </summary>
    public abstract class ParameterEntity
    {
        //protected Dictionary<ParameterIds, Parameter> parameters = new Dictionary<ParameterIds, Parameter>();
        //protected HashSet<string> triggers = new HashSet<string>();

        public Dictionary<ParameterIds, Parameter> Parameters { get; set; } = new Dictionary<ParameterIds, Parameter>();

        public List<Parameter> ParameterList
        {
            get
            {
                List<Parameter> list = new List<Parameter>(Parameters.Count);
                foreach (KeyValuePair<ParameterIds, Parameter> kvp in Parameters)
                    list.Add(kvp.Value);
                return list;
            }
        }

        public void AddParameter(Parameter parameter) => Parameters.Add(parameter.Id, parameter);

        public HashSet<string> Triggers { get; set; } = new HashSet<string>();

        public void SetTrigger(string trigger) => Triggers.Add(trigger);

        public void ClearTrigger(string trigger) => Triggers.Remove(trigger);

        public bool IsTriggerSet(string trigger) => Triggers.Contains(trigger);
    }
}
