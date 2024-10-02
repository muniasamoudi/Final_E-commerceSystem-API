using Data_Layer.DataModel;
using E_commerceSystem_API.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class DataSeeder
    {
        private readonly ECommerceDbContext _context;
        public DataSeeder(ECommerceDbContext context)
        {
            _context = context;
            //_context.Database.EnsureCreated();
        }

        public void Seed()
        {
            try
            {
                if (!_context.Customers.Any())
                {
                    var customer1 = new Customer { Name = "Loor" };
                    var customer2 = new Customer { Name = "Munia" };

                    _context.Customers.AddRange(customer1, customer2);
                    _context.SaveChanges();

                    var shoppingCart1 = new ShoppingCart { OwnerId = customer1.Id };
                    var shoppingCart2 = new ShoppingCart { OwnerId = customer2.Id };
                                       
                    _context.Carts.AddRange(shoppingCart1, shoppingCart2);
                    _context.SaveChanges();

                    var product1 = new Product { productName = "Laptop", productPrice = 500 };
                    var product2 = new Product { productName = "Phone", productPrice = 350 };
                    var product3 = new Product { productName = "Headphones", productPrice = 970 };

                    _context.Products.AddRange(product1, product2, product3);
                    _context.SaveChanges();
                    var orders = new List<Order>
                    {
                        new Order { customer = customer1, orderDate = DateTime.Now, customerID = customer1.Id, products = new List<Product> { product1, product2 } },
                        new Order { customer =  customer2, orderDate = DateTime.Now, customerID = customer2.Id, products = new List<Product> {product3} }
                    };

                    _context.Orders.AddRange(orders);
                    _context.SaveChanges();
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error during Seeding: " + ex.Message);
                throw;
            }               
        }
    }
}
