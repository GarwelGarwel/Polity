using System;

namespace Polity
{
    public class ProductType : IComparable<ProductType>
    {

        public string Name { get; set; }

        public override string ToString() => Name;

        double laborCost = 1;
        /// <summary>
        /// How many man-days needed to produce 1 item
        /// </summary>
        public double LaborCost
        {
            get => laborCost;
            set { if (value > 0) laborCost = value; }
        }

        double dailyNeed = 1;
        /// <summary>
        /// How much product every Person needs per day
        /// </summary>
        public double DailyNeed
        {
            get => dailyNeed;
            set { if (value >= 0) dailyNeed = value; }
        }

        public int CompareTo(ProductType pt) => Name.CompareTo(pt.Name);

        public ProductType() { }

        public ProductType(string name) => Name = name;

        public ProductType(string name, double laborCost, double dailyNeed)
        {
            Name = name;
            LaborCost = laborCost;
            DailyNeed = dailyNeed;
        }
    }
}
