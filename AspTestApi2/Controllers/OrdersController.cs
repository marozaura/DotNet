using AspTestApi2.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace AspTestApi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreatedOrder(OrderDto orderDto)
        {
            await _publishEndpoint.Publish<IOrderCreated>(new
            {
                Id = 1,
                orderDto.ProductName,
                orderDto.Quantity,
                orderDto.Price
            });

            return Ok();
        }
    }

  
}