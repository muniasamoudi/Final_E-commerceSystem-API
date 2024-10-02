using Business_Layer.Services.intf;
using Data_Layer.DataModel;
using Data_Layer.Repo.intf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.impl
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _OrderRepository;

        public OrderService(IRepository<Order> OrderRepository)
        {
            _OrderRepository = OrderRepository;
        }
        public async Task AddOrder(Order order)
        {
            await _OrderRepository.Add(order);
        }

        public async Task DeleteOrder(int id)
        {
            await _OrderRepository.Delete(id);
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _OrderRepository.GetById(id);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _OrderRepository.GetAll();
        }

        public async Task UpdateOrder(Order order)
        {
            await _OrderRepository.Update(order);
        }
    }
}
