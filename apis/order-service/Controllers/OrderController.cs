using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using order_service.Repositories.OrderReposity;

namespace order_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderReposity;
        
        public OrderController(IOrderRepository orderReposity)
        {
            _orderReposity = orderReposity;
        }
        
        // get all orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderReposity.GetAllOrders();
            return Ok(result);
        }
    }
}
