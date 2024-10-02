using Business_Layer.Services.intf;
using Data_Layer.DataModel;
using Data_Layer.Repo.intf;
using E_commerceSystem_API.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.impl
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _ProductRepository;
        public ProductService(IRepository<Product> repository)
        {
            _ProductRepository = repository;
        }
        public async Task AddProduct(Product product)
        {
            await _ProductRepository.Add(product);
        }

        public async Task DeleteProduct(int id)
        {
            await _ProductRepository.Delete(id);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _ProductRepository.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _ProductRepository.GetAll();
        }

        public async Task UpdateProduct(Product product)
        {
            await _ProductRepository.Update(product);
        }
    }
}
