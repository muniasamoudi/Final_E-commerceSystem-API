using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using E_commerceSystem_API.ViewModels;
using Business_Layer.Services.intf;
using Data_Layer.DataModel;

namespace E_commerceSystem_API.Controllers
{
    [Authorize] // Requires authentication for all actions in this controller
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;

        public OrderController(IOrderService orderService, ILogger logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // Publicly accessible endpoint (no authentication required)
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            var orderViewModels = orders.Select(o => new OrderViewModel
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
            }).ToList();
            return Ok(orderViewModels);
        }

        // Authenticated endpoint (requires valid JWT token)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                if (order == null)
                {
                    _logger.LogWarning("Order not found with ID {id}", id);
                    return NotFound(new { Message = $"Order not found with ID {id}" });
                }

                var orderViewModel = new OrderViewModel
                {
                    orderID = order.orderID,
                    customerID = order.customerID,
                    orderDate = order.orderDate,
                    products = order.products.Select(p => new ProductViewModel
                    {
                        productID = p.productID,
                        productName = p.productName,
                        productPrice = p.productPrice
                    }).ToList()
                };
                return Ok(orderViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving order with ID {id}", id);
                throw;
            }
        }

        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _orderService.AddOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.orderID }, order);
        }

        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.orderID)
            {
                return BadRequest();
            }

            /* var updateResult = await _orderService.UpdateOrder(order);
             if (!updateResult)
             {
                 return NotFound(new { Message = $"Order with ID {id} not found or could not be updated" });
             }*/
            await _orderService.UpdateOrder(order);
            return NoContent();
        }

        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            /* var result = await _orderService.DeleteOrder(id);
             if (!result)
             {
                 return NotFound(new { Message = $"Order with ID {id} not found or could not be deleted" });
             }*/
            await _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
