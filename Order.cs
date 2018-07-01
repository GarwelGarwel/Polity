namespace Polity
{
    public class Order
    {
        public enum OrderType { Purchase, Sale };

        public Person Customer { get; set; } = null;

        public Product Content { get; set; } = null;

        public OrderType Type { get; set; } = OrderType.Purchase;

        public Order() { }

        public Order(Person customer, Product content)
        {
            Customer = customer;
            Content = content;
        }

        public Order(Person customer, Product content, OrderType type)
        {
            Customer = customer;
            Content = content;
            Type = type;
        }
    }
}
