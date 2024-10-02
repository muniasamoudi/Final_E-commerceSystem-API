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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _CustomerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _CustomerRepository = customerRepository;
        }

        public async Task AddCustomer(Customer customer)
        {
            await _CustomerRepository.Add(customer);
        }

        public async Task DeleteCustomer(int id)
        {
            await _CustomerRepository.Delete(id);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _CustomerRepository.GetById(id);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _CustomerRepository.GetAll();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await _CustomerRepository.Update(customer);
        }
    }
}
