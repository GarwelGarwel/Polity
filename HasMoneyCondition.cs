namespace Polity
{
    /// <summary>
    /// Checks if an EconomicAgent has a certain amount of money
    /// </summary>
    public class HasMoneyCondition : Condition
    {
        public enum Types { MoreOrEqual, More, LessOrEqual, Less, Equal, NotEqual };

        public EconomicAgent Subject { get; set; }

        public EconomicAgent Subject1
        {
            get => Subject;
            set => Subject = value;
        }

        public EconomicAgent Subject2 { get; set; }

        public double Amount { get; set; }

        protected double Amount1 => Subject1.Money;

        protected double Amount2 => (Subject2 == null) ? Amount : Subject2.Money + Amount;

        public Types Type { get; set; } = Types.MoreOrEqual;

        protected override bool Check()
        {
            switch (Type)
            {
                case Types.MoreOrEqual: return Amount1 >= Amount2;
                case Types.More: return Amount1 > Amount2;
                case Types.LessOrEqual: return Amount1 <= Amount2;
                case Types.Less: return Amount1 < Amount2;
                case Types.Equal: return Amount1 == Amount2;
                case Types.NotEqual: return Amount1 != Amount2;
            }
            return false;
        }

        public override string ToString()
        {
            string res = Subject1.ToString() + " has " + base.ToString();
            switch (Type)
            {
                case Types.MoreOrEqual: res += ">= "; break;
                case Types.More: res += "> "; break;
                case Types.LessOrEqual: res += "<= "; break;
                case Types.Less: res += "< "; break;
            }
            res += "$" + Amount2;
            return res;
        }

        public HasMoneyCondition() { }

        public HasMoneyCondition(EconomicAgent subject, double amount)
        {
            Subject = subject;
            Amount = amount;
        }

        public HasMoneyCondition(EconomicAgent subject, Types type, double amount)
        {
            Subject = subject;
            Type = type;
            Amount = amount;
        }

        public HasMoneyCondition(EconomicAgent subject1, Types type, EconomicAgent subject2)
        {
            Subject = subject1;
            Type = type;
            Subject2 = subject2;
        }
    }
}
