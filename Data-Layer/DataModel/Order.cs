using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data_Layer.DataModel
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered
    }
    public class Order
    {
        public int orderID { get; set; }
        public Customer customer { get; set; }
        public int customerID { get; set; } // Foreign key
        public List<Product> products { get; set; } = new List<Product>();
        public DateTime orderDate { get; set; }
        public OrderStatus status { get; set; }
        public Order() { }

        public Order(Customer customer, List<Product> products)
        {
            customer = customer;
            products = new List<Product>(products);
        }
      
        public Order(Customer customer, List<Product> products, DateTime date, OrderStatus status)
        {
            customer = customer;
            products = new List<Product>(products);
            orderDate = date;
            status = status;
            
        }
       
        public double ApplyDiscount(double discount)
        {
            return discount * 0.9;
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

        public double CalculateOrder()
        {
            return products.Sum(item => item.productPrice);

        }

    }
}
