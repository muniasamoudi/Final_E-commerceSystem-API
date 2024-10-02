using Business_Layer.Services.impl;
using Business_Layer.Services.intf;
using Data_Layer.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using E_commerceSystem_API.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerceSystem_API.Controllers
{
    [Authorize] // Global authorization
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productservice;
        private readonly ILogger _logger;

        public ProductController(IProductService productservice, ILogger logger)
        {
            _productservice = productservice;
            _logger = logger;
        }

        [AllowAnonymous] // Publicly accessible to fetch all products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productservice.GetProducts();
            var productViewModels = products.Select(p => new ProductViewModel
            {
                productID = p.productID,
                productName = p.productName,
                productPrice = p.productPrice,
            }).ToList();

            return Ok(productViewModels);
        }

        [AllowAnonymous] // Publicly accessible to fetch product by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productservice.GetProductById(id);
                if (product == null)
                {
                    _logger.LogWarning("Product not found with ID {id}", id);
                    return NotFound(new { Message = $"Product not found with ID {id}" });
                }

                var productViewModel = new ProductViewModel
                {
                    productID = product.productID,
                    productName = product.productName,
                    productPrice = product.productPrice,
                };

                return Ok(productViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving product with ID {id}", id);
                throw;
            }
        }

        [Authorize(Roles = "Admin")] // Admin-only access to add a product
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productservice.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.productID }, product);
        }

        [Authorize(Roles = "Admin")] // Admin-only access to update a product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.productID) { return BadRequest(); }
            // var updateResult = await _productservice.UpdateProduct(product);
            //if (!updateResult)
            // {
            //     return NotFound(new { Message = "Product with ID {id} not found or could not be updated" });
            //  }
            await _productservice.UpdateProduct(product);
            return NoContent();
        }

        [Authorize(Roles = "Admin")] // Admin-only access to delete a product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //   var result = await _productservice.DeleteProduct(id);
            await _productservice.DeleteProduct(id);
            /*  if (!result)
              {
                  return NotFound(new { Message = $"Product with ID {id} not found or could not be deleted" });
              }*/

            return NoContent();
        }
    }
}
