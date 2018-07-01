using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polity
{
    /// <summary>
    /// Describes Person's Occupation (e.g., Worker, Bureaucrat, Deputy, etc.) and ProductType or other details
    /// </summary>
    public class Job
    {
        public enum Occupations { Worker, GoldMiner, Bureaucrat, Deputy };

        ProductType productType;  // For workers
        Party party;  // For deputies

        public Occupations Occupation { get; set; }

        public ProductType ProductType
        {
            get => (Occupation == Occupations.Worker) ? productType : null;
            set => productType = value;
        }

        public Party Party
        {
            get => (Occupation == Occupations.Deputy) ? party : null;
            set => party = value;
        }

        public override string ToString()
        {
            switch (Occupation)
            {
                case Occupations.Worker: return "Worker (" + ProductType + ")";
                case Occupations.GoldMiner: return "Gold Miner";
                case Occupations.Bureaucrat: return "Bureaucrat";
                case Occupations.Deputy: return "MP (" + Party + ")";
            }
            return "";
        }

        public Job() { }

        public Job(Occupations occupation) => Occupation = occupation;

        public Job(Occupations occupation, ProductType productType)
        {
            Occupation = occupation;
            ProductType = productType;
        }
    }
}
