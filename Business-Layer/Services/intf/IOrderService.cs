using Data_Layer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.intf
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        Task AddOrder(Order order);
        Task DeleteOrder(int id);
        Task UpdateOrder(Order order);
    }
}
