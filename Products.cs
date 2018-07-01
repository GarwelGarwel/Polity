using System.Collections.Generic;

namespace Polity
{
    public class Products
    {
        SortedList<ProductType, double> products = new SortedList<ProductType,double>();

        public List<Product> Items
        {
            get
            {
                List<Product> res = new List<Product>(products.Count);
                foreach (KeyValuePair<ProductType, double> kvp in products)
                    res.Add(new Product(kvp.Key, kvp.Value));
                return res;
            }
        }

        public Product this[ProductType pt]
        {
            get
            {
                if (products.ContainsKey(pt))
                    return new Product(pt, products[pt]);
                else return new Product(pt, 0);
            }
            set
            {
                if (value == null)
                {
                    RemoveAll(pt);
                    return;
                }
                if (value.Type != pt)
                    throw new System.ArgumentException("Wrong product types in Products[]");
                if (products.ContainsKey(pt)) products[pt] = value.Amount;
                else products.Add(pt, value.Amount);
            }
        }

        public void Add(Product p)
        {
            if (products.ContainsKey(p.Type)) products[p.Type] += p.Amount;
            else products.Add(p.Type, p.Amount);
        }

        public void Remove(Product p)
        {
            if (products.ContainsKey(p.Type)) products[p.Type] += p.Amount;
            else products.Add(p.Type, - p.Amount);
        }

        public void RemoveAll(ProductType pt) => products.Remove(pt);

        public int Count => products.Count;

        public Products() { }

        public Products(Products p) => products = p.products;
    }
}
