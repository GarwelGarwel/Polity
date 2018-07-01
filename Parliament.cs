using System;
using System.Collections.Generic;

namespace Polity
{
    public class Parliament
    {
        public Country Country { get; set; }

        public List<Person> Deputies { get; set; } = new List<Person>();

        public void AddDeputy(Person p)
        {
            p.Job = new Job(Job.Occupations.Deputy);
            p.Job.Party = Country.Parties[Game.R.Next(Country.Parties.Count)];
            Deputies.Add(p);
        }

        public void Clear()
        {
            foreach (Person p in Deputies)
                p.GetNewJob();
            Deputies.Clear();
        }

        /// <summary>
        /// Returns true if Bill is approved, false if not
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public bool Vote(Bill bill)
        {
            double a;
            int yea = 0, nay = 0, abstain = 0;
            Game.TheGame.AppendLog("Parliament is voting on " + bill.Name);
            foreach (Person p in Deputies)
            {
                a = p.Job.Party.Platform * bill.Politics;
                if (a > 0) yea++;
                else if (a < 0) nay++;
                else abstain++;
            }
            Game.TheGame.AppendLog("Yea: " + yea + ". Nay: " + nay + ". Abstain: " + abstain);
            bool res = yea > (bill.VotesNeeded * Country.ParliamentSize.EffectiveValue);
            if (res) Game.TheGame.AppendLog("Bill passed.");
            else Game.TheGame.AppendLog("Bill rejected.");
            return res;
        }

        public Parliament() { }

        public Parliament(Country country, int num)
        {
            Country = country;
            int j;
            for (int i = 0; i < Math.Min(num, Country.Population); i++)
            {
                do
                {
                    j = (int)Math.Floor(Game.R.NextDouble() * Country.Population);
                }
                while (Country.People[j].Job.Occupation == Job.Occupations.Deputy);
                AddDeputy(Country.People[j]);
            }
        }
    }
}
