namespace Polity
{
    public class EconomicAgent : ParameterEntity
    {
        double dayIncome, dayExpenses;

        public double Money { get; set; }

        public string MoneyString => GetMoneyString(Money);

        public override string ToString() => MoneyString;

        public double DayIncome => dayIncome;

        public string DayIncomeString => GetMoneyString(DayIncome);

        public double DayExpenses => dayExpenses;

        public string DayExpensesString => GetMoneyString(dayExpenses);

        public static string GetMoneyString(double amount) => "$" + amount.ToString("F2");

        public void Pay(double amount)
        {
            Money -= amount;
            dayExpenses += amount;
        }

        public void Pay(double amount, EconomicAgent whom)
        {
            Pay(amount);
            whom.Receive(amount);
        }

        public void Receive(double amount)
        {
            Money += amount;
            dayIncome += amount;
        }

        public void NextTurn() => dayIncome = dayExpenses = 0;

        public EconomicAgent() { }

        public EconomicAgent(double money) => Money = money;
    }
}
