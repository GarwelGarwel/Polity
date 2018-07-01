namespace Polity
{
    public class Product
    {
        public ProductType Type { get; set; }

        public double Amount { get; set; }

        public override string ToString() => Type.Name + " (" + Amount + ")";

        public Product() { }

        public Product(ProductType type, double amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}
