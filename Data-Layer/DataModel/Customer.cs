using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data_Layer.DataModel
{
    public class Customer : Person
    {
      
        public int customerId { get; set; }
        public ICollection<Order> orders {  get; set; } = new List<Order>();
        [JsonIgnore]
        public ShoppingCart Cart { get; set; }
        public Customer()
        {
            Cart = new ShoppingCart(this);
        }
        public Customer(int id, string name) : base(id, name)
        {
            Cart = new ShoppingCart(this);
        }
        public Customer(int id, string name, ShoppingCart cart) : base(id, name)
        {
            Cart = cart;
        }

        public Order PlaceOrder()
        {
            Order order = new Order(this, Cart.Products);
            return order;
        }

        public void AddToCart(Product product)
        {
            Cart.AddProduct(product);
        }

        public void RemoveFromCart(Product product)
        {
            Cart.RemoveProduct(product);
        }
    }
}
