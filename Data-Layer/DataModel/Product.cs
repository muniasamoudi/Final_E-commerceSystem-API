using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.DataModel
{
    public class Product
    {
        public int productID { set; get; }
        public string productName { set; get; }
        public double productPrice { set; get; }
        public ICollection<Order> orders { get; set; } = new List<Order>();

        public Product() { }
        public Product(int id, string name, double price)
        {
            productID = id;
            productName = name;
            productPrice = price;
        }

        public void GetProductInfo()
        {
            Console.WriteLine("ID : " + productID + " , Name : " + productName + " , Price : " +productPrice);
        }
        public void UpdatePrice(double price)
        {
            productPrice = price;
        }
    }
}
