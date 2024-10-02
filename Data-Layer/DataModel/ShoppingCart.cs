using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.DataModel
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int OwnerId {  get; set; }
        public List<Product> Products { get; set; }
        public Customer Owner { get; set; }

        public ShoppingCart() { }
        public ShoppingCart(Customer customer)
        {
            Owner = customer;
            Products = new List<Product>(); // Tight Coupling
        }
        public void AddProduct(Product product)
        {
            try
            {
                if (product != null)
                {
                    Products.Add(product);
                    Console.WriteLine("Add Product to Cart");
                }
                else { return; }


            }
            catch (Exception e) { Console.WriteLine(e); }

        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
    }
}
