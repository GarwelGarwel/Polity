using System;
using System.Collections.Generic;

namespace Polity
{
    public enum ParameterIds {
        // Game-wide parameters
        HappinessSensitivity,
        // Country-specific parameters
        IncomeTaxRate, WelfareSubsidy, BureaucratSalary, OptimalBureaucracySize, PercentBureaucrats, ChanceConsiderJobChange, PartyDiscipline, ParliamentSize, GoldLaborCost,
        // Country- & person-specific parameters (added for effective value)
        Productivity,
        // Person-specific parameters
        Happiness,
        // Party-specific parameters
        Loyalty
    };

    public class Game : ParameterEntity
    {
        public static int StartPopulation = 100;
        public static Random R = new Random();

        public Game()
        {
            AddParameter(new Parameter(ParameterIds.HappinessSensitivity, "Happiness Sensitivity", 0.25, 0, 1));  // How quickly happiness follows Satisfaction, 0: never, 1: immediately

            Date = new Date(2013, 1, 1);

            ProductTypes.Add(new ProductType("Food", 0.5, 1));
            ProductTypes.Add(new ProductType("Clothes", 1, 1));

            Country = new Polity.Country(StartPopulation);

            Event e = new Event("Discovery");
            e.Condition = new ChanceCondition(0.01);
            e.Effect = new MultipleEffects();
            e.AddEffect(new MessageEffect("New technologies increase productivity!"));
            e.AddEffect(new ChangeParameterEffect(Country, ParameterIds.Productivity, 1, 0.05));

            e = new Event("MPs Propose to Lower Taxes");
            e.Condition = new ChanceCondition(0.3);
            e.HappensOnce = true;
            Issues iss = new Issues();
            iss.AddIssue(IssueIds.Populism, 1);
            iss.AddIssue(IssueIds.BigGovernment, -1);
            e.Effect = new SubmitBillEffect(new Bill(iss, new ChangeParameterEffect(Country, ParameterIds.IncomeTaxRate, 1, -0.05)), Country.Parliament);
            Events.Add(e);

            Decision d = new Decision("Celebrate");
            d.DisplayCondition = new HasMoneyCondition(Country.Budget, 5);
            d.Effect = new MessageEffect("We have lots of money! Hurray!");
            Decisions.Add(d);

            d = new Decision("Decrease Income Tax");
            iss = new Issues();
            iss.AddIssue(IssueIds.Populism, 0.5);
            iss.AddIssue(IssueIds.BigGovernment, -0.5);
            d.Effect = new SubmitBillEffect(new Bill(iss, new ChangeParameterEffect(Country, ParameterIds.IncomeTaxRate, 1, -0.02)), Country.Parliament);
            Decisions.Add(d);

            d = new Decision("Increase Income Tax");
            iss = new Issues();
            iss.AddIssue(IssueIds.Populism, -0.5);
            iss.AddIssue(IssueIds.BigGovernment, 0.5);
            d.Effect = new SubmitBillEffect(new Bill(iss, new ChangeParameterEffect(Country, ParameterIds.IncomeTaxRate, 1, 0.02)), Country.Parliament);
            Decisions.Add(d);

            d = new Decision("See Invisible Pink Unicorn");
            d.DisplayCondition = new ConstCondition(false);
            d.Effect = new MessageEffect("Wow! Here it is: the invisible pink unicorn");
            Decisions.Add(d);
        }

        static Game theGame;
        public static Game TheGame
        {
            get
            {
                if (theGame == null) theGame = new Game();
                return theGame;
            }
        }
        public Date Date { get; set; }
        public Country Country { get; set; }
        public static List<ProductType> ProductTypes { get; set; } = new List<ProductType>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Decision> Decisions { get; set; } = new List<Decision>();

        public List<Decision> DisplayedDecisions
        {
            get
            {
                List<Decision> res = new List<Decision>();
                foreach (Decision d in Decisions)
                    if (d.CheckDisplay) res.Add(d);
                return res;
            }
        }

        public string Log { get; set; } = "";

        public void AppendLog(string line) => Log = Char.ToUpper(line[0]) + line.Substring(1) + "\r\n" + Log;

        public void AppendLog() => Log = "\r\n" + Log;

        public void ClearLog() => Log = "";

        public Parameter HappinessSensitivity
        {
            get => Parameters[ParameterIds.HappinessSensitivity];
            set => Parameters[ParameterIds.HappinessSensitivity] = value;
        }

        public void NextTurn()
        {
            Date.NextTurn();
            AppendLog(Date.ToString());
            Country.NextTurn();
            foreach (Event e in Events)
                e.CheckAndRun();
        }
    }
}
