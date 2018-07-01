using System;
using System.Collections.Generic;
using System.Linq;

namespace Polity
{
    public class Person : EconomicAgent
    {
        public static string[] FirstNames = { "Ivan", "John", "Michael", "George", "Alex", "Robert", "Mark", "Bruce" };
        public static string[] LastNames = { "Jackson", "Johnson", "Smith", "Williams" };

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name => FirstName + " " + LastName;

        public override string ToString() => Name;

        public Products Property { get; set; } = new Products();

        public void Buy(Product p, Market m)
        {
            Property.Add(p);
            Pay(p.Amount * m.Prices[p.Type]);
        }

        public Job Job { get; set; }

        public Product Production
        {
            get
            {
                if (Job.Occupation == Job.Occupations.Worker)
                    return new Product(Job.ProductType, ProductivityEffective / Job.ProductType.LaborCost);
                else return null;
            }
            set => Job.ProductType = value.Type;
        }

        public double Satisfaction { get; set; }

        public string SatisfactionString => Math.Round(Satisfaction * 100).ToString() + "%";

        public Country Citizenship { get; set; }

        public Parameter Productivity
        {
            get => Parameters[ParameterIds.Productivity];
            set => Parameters[ParameterIds.Productivity] = value;
        }

        public double ProductivityEffective => Citizenship.Productivity.Value + Productivity.Value;

        public Parameter Happiness
        {
            get => Parameters[ParameterIds.Happiness];
            set => Parameters[ParameterIds.Happiness] = value;
        }

        public string HappinessString => Happiness.Percentage;

        public Job GetNewJob()
        {
            SortedList<double, Job> profitabilities = new SortedList<double, Job>();
            foreach (ProductType pt in Game.ProductTypes)
                profitabilities.Add(Game.TheGame.Country.Market.Prices[pt] / pt.LaborCost * ProductivityEffective, new Job(Job.Occupations.Worker, pt));
            profitabilities.Add(1 / Citizenship.GoldLaborCost.Value, new Job(Job.Occupations.GoldMiner));
            profitabilities.Add(Citizenship.BureaucratSalary.Value, new Job(Job.Occupations.Bureaucrat));  // Bureaucrat's pay
            Job newJob = profitabilities.Last().Value;
            Game.TheGame.AppendLog(Name + " has changed his/her job from " + Job + " to " + newJob + ".");
            return newJob;
        }

        public new void NextTurn()
        {
            base.NextTurn();

            double curSat = 0;
            int needs = 0;
            foreach (ProductType pt in Game.ProductTypes)
            {
                if (pt.DailyNeed == 0) continue;
                curSat += Math.Min(Math.Sqrt(Math.Max(Property[pt].Amount / pt.DailyNeed, 0)), 1);
                needs++;
                if (curSat == 1) Property.Remove(new Product(pt, pt.DailyNeed));
                else Property.RemoveAll(pt);
            }
            if (needs != 0) Satisfaction = curSat / needs;
            else Satisfaction = 1;
            if ((Satisfaction < 1) && (Job.Occupation != Job.Occupations.Deputy) && (Game.R.NextDouble() < Citizenship.ChanceConsiderJobChange.Value))
                Job = GetNewJob();
            Happiness.Value = Happiness.Value * (1 - Game.TheGame.HappinessSensitivity.Value) + Satisfaction * Game.TheGame.HappinessSensitivity.Value;
        }

        public Person(Country citizenship)
        {
            Citizenship = citizenship;
            FirstName = FirstNames[Game.R.Next(FirstNames.Length)];
            LastName = LastNames[Game.R.Next(LastNames.Length)];
            Money = Game.R.Next(0, 5);
            if (Game.R.NextDouble() < Citizenship.PercentBureaucrats.Value)
                Job = new Job(Job.Occupations.Bureaucrat);
            else Job = new Job(Job.Occupations.Worker, Game.ProductTypes[Game.R.Next(Game.ProductTypes.Count)]);

            Parameters.Add(ParameterIds.Productivity, new Parameter(ParameterIds.Productivity, "Productivity", 0));  // How many man-days a normal worker makes
            Parameters.Add(ParameterIds.Happiness, new Parameter(ParameterIds.Happiness, "Happiness", Game.R.NextDouble(), 0, 1));  // How many man-days a normal worker makes
        }
    }
}
