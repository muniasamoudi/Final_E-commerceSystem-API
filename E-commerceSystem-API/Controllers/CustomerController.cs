using Business_Layer.Services.intf;
using Data_Layer.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using E_commerceSystem_API.ViewModels;

namespace E_commerceSystem_API.Controllers
{
    [Authorize] // Applies authorization to all actions unless overridden by [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        // Publicly accessible endpoint (No authentication required)
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomers();
            if (customers == null || !customers.Any())
            {
                return NotFound();
            }

            var customerViewModels = customers.Select(c => new CustomerViewModel
            {
                customerID = c.Id,
                customerName = c.Name,
                Orders = c.orders.Select(o => new OrderViewModel
                {
                    orderID = o.orderID,
                    customerID = o.customerID,
                    orderDate = o.orderDate,
                    products = o.products.Select(p => new ProductViewModel
                    {
                        productID = p.productID,
                        productName = p.productName,
                        productPrice = p.productPrice
                    }).ToList()
                }).ToList()
            }).ToList();

            return Ok(customerViewModels);
        }

        // Authenticated endpoint (Requires valid JWT token)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    _logger.LogWarning("Customer not found with ID {id}", id);
                    return NotFound(new { Message = $"Customer not found with ID {id}" });
                }

                var customerViewModel = new CustomerViewModel
                {
                    customerID = id,
                    customerName = customer.Name,
                    Orders = customer.orders.Select(o => new OrderViewModel
                    {
                        orderID = o.orderID,
                        customerID = o.customerID,
                        orderDate = o.orderDate,
                        products = o.products.Select(p => new ProductViewModel
                        {
                            productID = p.productID,
                            productName = p.productName,
                            productPrice = p.productPrice
                        }).ToList()
                    }).ToList()
                };

                return Ok(customerViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving customer with ID {id}", id);
                throw;
            }
        }

        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _customerService.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest("Customer data is invalid.");
            }

            /* var result = await _customerService.UpdateCustomer(customer);
             if (!result)
             {
                 return NotFound(new { Message = $"Customer with ID {id} not found or could not be updated" });
             }*/
            await _customerService.UpdateCustomer(customer);
            return NoContent();
        }

        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {/*
            var result = await _customerService.DeleteCustomer(id);
            if (!result)
            {
                return NotFound(new { Message = $"Customer with ID {id} not found or could not be deleted" });
            }*/
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
