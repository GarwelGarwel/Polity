using System;

namespace Polity
{
    public class Parameter
    {
        public ParameterIds Id { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public double MinValue { get; set; } = double.MinValue;

        public double MaxValue { get; set; } = double.MaxValue;

        public double EffectiveValue => Math.Min(Math.Max(Value, MinValue), MaxValue);

        public string Percentage => Math.Round(Value * 100).ToString() + "%";

        public override string ToString() => Name;

        public Parameter() { }

        public Parameter(ParameterIds id) => Id = id;

        public Parameter(ParameterIds id, double value)
        {
            Id = id;
            Value = value;
        }

        public Parameter(ParameterIds id, string name, double value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public Parameter(ParameterIds id, string name, double value, double minValue, double maxValue)
        {
            Id = id;
            Name = name;
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}
