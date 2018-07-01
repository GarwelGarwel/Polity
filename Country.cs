using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Polity
{
    public class Country : ParameterEntity
    {
        public enum EconomySystems { Communism, Basic };

        public Country(int startPopulation)
        {
            AddParameter(new Parameter(ParameterIds.BureaucratSalary, "Bureaucrat Salary", 0));  // How much money every Person whose Job is Bureaucrat receives from the Budget
            AddParameter(new Parameter(ParameterIds.ChanceConsiderJobChange, "Chance to Consider Job Change", 0.02));  // Daily chance a Person will consider other jobs (productionTypes)
            AddParameter(new Parameter(ParameterIds.IncomeTaxRate, "Income Tax Rate", 0));    // % of personal income that goes to budget
            AddParameter(new Parameter(ParameterIds.OptimalBureaucracySize, "Optimal Bureaucracy Size", 0.05));  // How many bureaucrats needed for optimal AdministativeEfficiency
            AddParameter(new Parameter(ParameterIds.PercentBureaucrats, "Initial Percent of Bureaucrats", 0.05));  // How many Bureaucrats there are initially
            AddParameter(new Parameter(ParameterIds.WelfareSubsidy, "Welfare Subsidy", 0));  // Money paid by the budget to every Person every day
            AddParameter(new Parameter(ParameterIds.PartyDiscipline, "Party Discipline", 0.5));  // To what extent MPs' votes are affected by party positions vs. personal opinions
            AddParameter(new Parameter(ParameterIds.ParliamentSize, "Parliament Size", 10));  // How many Deputies in the Parliament
            AddParameter(new Parameter(ParameterIds.GoldLaborCost, "Gold Labor Cost", 10));  // How many days it takes to create $1 for GoldMiner
            AddParameter(new Parameter(ParameterIds.Productivity, "Nationwide Productivity", 1));  // How many man-days a normal worker makes
            
            People = new ObservableCollection<Person>();
            for (int i = 0; i < startPopulation; i++)
                People.Add(new Person(this));
            Budget = new EconomicAgent(Population * (WelfareSubsidy.Value + PercentBureaucrats.Value * BureaucratSalary.Value) * (0.5 + Game.R.NextDouble()));

            Party party = new Party();
            party.Name = "Social-Democratic Party";
            party.Platform.AddIssue(IssueIds.Populism, 0.5);
            party.Platform.AddIssue(IssueIds.BigGovernment, 0.5);
            Parties.Add(party);

            party = new Party();
            party.Name = "Conservative Party";
            party.Platform.AddIssue(IssueIds.Populism, 0.4);
            party.Platform.AddIssue(IssueIds.BigGovernment, -0.5);
            Parties.Add(party);

            Parliament = new Parliament(this, (int) ParliamentSize.Value);
        }

        public string Name { get; set; } = "Politland";

        public ObservableCollection<Person> People { get; set; }

        public int Population => People.Count;

        public Market Market { get; set; } = new Market();

        /// <summary>
        /// Basic: individuals sell to & buy from market; Communism: moneyless equal distribution
        /// </summary>
        public EconomySystems EconomySystem { get; set; } = EconomySystems.Basic;

        public EconomicAgent Budget { get; set; }

        public Parameter IncomeTaxRate
        {
            get => Parameters[ParameterIds.IncomeTaxRate];
            set => Parameters[ParameterIds.IncomeTaxRate] = value;
        }

        public Parameter WelfareSubsidy
        {
            get => Parameters[ParameterIds.WelfareSubsidy];
            set => Parameters[ParameterIds.WelfareSubsidy] = value;
        }

        public Parameter BureaucratSalary
        {
            get => Parameters[ParameterIds.BureaucratSalary];
            set => Parameters[ParameterIds.BureaucratSalary] = value;
        }

        public Parameter PercentBureaucrats
        {
            get => Parameters[ParameterIds.PercentBureaucrats];
            set => Parameters[ParameterIds.PercentBureaucrats] = value;
        }

        public Parameter ChanceConsiderJobChange
        {
            get => Parameters[ParameterIds.ChanceConsiderJobChange];
            set => Parameters[ParameterIds.ChanceConsiderJobChange] = value;
        }

        public Parameter OptimalBureaucracySize
        {
            get => Parameters[ParameterIds.OptimalBureaucracySize];
            set => Parameters[ParameterIds.OptimalBureaucracySize] = value;
        }

        public Parameter PartyDiscipline
        {
            get => Parameters[ParameterIds.PartyDiscipline];
            set => Parameters[ParameterIds.PartyDiscipline] = value;
        }

        public Parameter ParliamentSize
        {
            get => Parameters[ParameterIds.ParliamentSize];
            set => Parameters[ParameterIds.ParliamentSize] = value;
        }

        public Parameter GoldLaborCost
        {
            get { return Parameters[ParameterIds.GoldLaborCost]; }
            set { Parameters[ParameterIds.GoldLaborCost] = value; }
        }

        public Parameter Productivity
        {
            get => Parameters[ParameterIds.Productivity];
            set => Parameters[ParameterIds.Productivity] = value;
        }

        public Parliament Parliament { get; set; }

        public List<Party> Parties { get; set; } = new List<Party>();

        public double AverageIncome
        {
            get
            {
                double res = 0;
                foreach (Person p in People)
                    res += p.DayIncome;
                return res / Population;
            }
        }

        public double AverageExpenses
        {
            get
            {
                double res = 0;
                foreach (Person p in People)
                    res += p.DayExpenses;
                return res / Population;
            }
        }

        public double AverageSatisfaction
        {
            get
            {
                double res = 0;
                foreach (Person p in People)
                    res += p.Satisfaction;
                return res / Population;
            }
        }

        public int GetJobsCount(Job.Occupations occupation)
        {
            int res = 0;
            foreach (Person p in People)
                if (p.Job.Occupation == occupation) res++;
            return res;
        }

        public double AdministrativeEfficiency => Math.Min(GetJobsCount(Job.Occupations.Bureaucrat) / OptimalBureaucracySize.Value, 1);

        public void NextTurn()
        {
            Budget.NextTurn();
            Market.NextTurn();
            switch (EconomySystem)
            {
                case EconomySystems.Communism:
                    NextTurnCommunism();
                    break;
                case EconomySystems.Basic:
                    NextTurnBasic();
                    break;
            }
            foreach (Person p in People)
                p.NextTurn();
        }

        public void NextTurnBasic()
        {
            double buy, productsNorm = 0, livingWage = 0, economyCoef;
            SortedList<ProductType, double> products = new SortedList<ProductType, double>(Game.ProductTypes.Count);

            // Making list of all needed Products, calculating their satisfaction-per-dollar rates
            foreach (ProductType pt in Game.ProductTypes)
                if (pt.DailyNeed > 0)
                {
                    products.Add(pt, 1 / (Market.Prices[pt] * pt.DailyNeed));
                    livingWage += Market.Prices[pt] * pt.DailyNeed;
                    productsNorm += 1 / (Market.Prices[pt] * pt.DailyNeed);
                }

            foreach (Person p in People)
            {
                // Adding Person's Production to Supply list if s/he is a Worker
                if (p.Job.Occupation == Job.Occupations.Worker) Market.AddSupply(p.Production, p);

                // If the Person is GoldMiner, add his income
                if (p.Job.Occupation == Job.Occupations.GoldMiner)
                    p.Receive(p.ProductivityEffective / GoldLaborCost.Value);

                // Now adding Person's wanted products to Demand list, cheapest products first
                if (p.Money <= 0) continue;
                economyCoef = Math.Min(p.Money / livingWage, 1);
                foreach (KeyValuePair<ProductType, double> kvp in products)
                {
                    buy = Math.Min(livingWage * economyCoef * (kvp.Value / productsNorm) / Market.Prices[kvp.Key], kvp.Key.DailyNeed);
                    Market.AddDemand(new Product(kvp.Key, buy), p);
                }
            }

            // Commit transactions
            Market.ExecuteOrders();

            // Pay state salaries and welfare subsidies to all citizens
            if (WelfareSubsidy.EffectiveValue == 0) economyCoef = 1;
            else economyCoef = Math.Min (Budget.Money / (WelfareSubsidy.EffectiveValue * Population), 1);
            foreach (Person p in People)
            {
                Budget.Pay(WelfareSubsidy.EffectiveValue * economyCoef * AdministrativeEfficiency, p);
                if ((p.Job.Occupation == Job.Occupations.Bureaucrat) || (p.Job.Occupation == Job.Occupations.Deputy)) Budget.Pay(BureaucratSalary.EffectiveValue, p);
            }

            foreach (ProductType pt in Game.ProductTypes)
                Game.TheGame.AppendLog(pt.Name + ": Supply = " + Math.Round(Market.Supply[pt].Amount, 2) + ". Demand = " + Math.Round(Market.Demand[pt].Amount, 2) + ". Price = " + Math.Round(Market.Prices[pt], 2));
            Game.TheGame.AppendLog("Total market trade volume: $" + Math.Round(Market.Volume, 2));
        }

        public void NextTurnCommunism()
        {
            foreach (Person p in People)
                Market.AddSupply(p.Production);
            foreach (ProductType pt in Game.ProductTypes)
                Market.AddDemand(new Product(pt, pt.DailyNeed * Population));
            foreach (Person p in People)
                foreach (ProductType pt in Game.ProductTypes)
                    p.Property.Add(new Product(pt, Market.Supply[pt].Amount / Population));
        }
    }
}
