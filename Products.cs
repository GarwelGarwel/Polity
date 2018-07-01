using System.Collections.Generic;

namespace Polity
{
    /// <summary>
    /// An inventory of Product items
    /// </summary>
    public class Products
    {
        SortedList<ProductType, double> products = new SortedList<ProductType,double>();

        public List<Product> Items => (List<Product>)products.Keys;

        public Product this[ProductType pt]
        {
            get => new Product(pt, products.ContainsKey(pt) ? products[pt] : 0);
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
            if (products.ContainsKey(p.Type)) products[p.Type] -= p.Amount;
            else products.Add(p.Type, -p.Amount);
        }

        public void RemoveAll(ProductType pt) => products.Remove(pt);

        /// <summary>
        /// Remove all records with 0 amount of product
        /// </summary>
        public void ClearEmpty()
        {
            foreach (KeyValuePair<ProductType, double> kvp in products)
                if (kvp.Value <= 0) products.Remove(kvp.Key);
        }

        public int Count => products.Count;

        public Products() { }

        public Products(Products p) => products = p.products;
    }
}
