using System;
using System.Collections.Generic;

namespace Polity
{
    public class Market
    {
        public Market()
        {
            // Initializing prices with random values 0.5 .. 1.5
            foreach (ProductType pt in Game.ProductTypes)
                Prices[pt] = 0.5 + Game.R.NextDouble();
        }

        public Products Supply { get; set; } = new Products();

        public Products Demand { get; set; } = new Products();

        public SortedList<ProductType, double> Prices { get; set; } = new SortedList<ProductType, double>();

        public List<Order> Orders { get; set; } = new List<Order>();

        public double Volume { get; protected set; }

        public void AddSupply(Product product) => Supply.Add(product);

        public void AddSupply(Product product, Person seller)
        {
            AddSupply(product);
            Orders.Add(new Order(seller, product, Order.OrderType.Sale));
        }

        public void AddDemand(Product product) => Demand.Add(product);

        public void AddDemand(Product product, Person buyer)
        {
            AddDemand(product);
            Orders.Add(new Order(buyer, product));
        }

        public double GetAvailability(ProductType pt) => Math.Min(Supply[pt].Amount / Demand[pt].Amount, 1);

        public void ExecuteOrders()
        {
            double amount;
            Volume = 0;
            foreach (Order o in Orders)
            {
                if (o.Type == Order.OrderType.Sale)
                {
                    amount = o.Content.Amount * Math.Min(Demand[o.Content.Type].Amount / Supply[o.Content.Type].Amount, 1) * Prices[o.Content.Type];
                    o.Customer.Receive(amount);  // Get paid for sold product
                    o.Customer.Pay(amount * o.Customer.Citizenship.IncomeTaxRate.Value * o.Customer.Citizenship.AdministrativeEfficiency, o.Customer.Citizenship.Budget);  // Pay income tax
                    Volume += amount;
                }
                else
                    o.Customer.Buy(new Product(o.Content.Type, o.Content.Amount * GetAvailability(o.Content.Type)), this);  // Provide available Product to Customer & get paid for it
            }
        }

        double GetPriceCorrectionCoefficient(double demandToSupply) => (demandToSupply > 1) ? Math.Min(Math.Sqrt(demandToSupply), 1.05) : Math.Max(Math.Sqrt(demandToSupply), 0.95);

        public void NextTurn()
        {
            double demandToSupply;

            Volume = 0;

            // Correcting prices depending on supply-to-demand ratio
            foreach (ProductType pt in Game.ProductTypes)
            {
                if ((Demand[pt].Amount == 0) && (Supply[pt].Amount == 0)) continue;
                if (Demand[pt].Amount >= Supply[pt].Amount * 100) demandToSupply = 100;
                else if (Supply[pt].Amount >= Demand[pt].Amount * 100) demandToSupply = 0.01;
                else demandToSupply = Demand[pt].Amount / Supply[pt].Amount;
                Prices[pt] *= GetPriceCorrectionCoefficient(demandToSupply);
            }

            // Cleaning up
            Orders.Clear();
            Supply = new Products();
            Demand = new Products();
        }
    }
}
