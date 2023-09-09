using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using order_service.Dtos.OrderDto;
using order_service.Repositories.OrderItemRepository;
using order_service.Repositories.OrderReposity;

namespace order_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderReposity;
        
        public OrderController(IOrderRepository orderReposity, IOrderItemRepository orderItemRepository)
        {
            _orderReposity = orderReposity;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var serviceRespone = await _orderReposity.GetAllOrders();
            if (serviceRespone.Data is null)
                return NotFound(serviceRespone);
            return Ok(serviceRespone);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var serviceRespone = await _orderReposity.GetOrderByOrderId(id);
            if (serviceRespone.Data is null)
                return NotFound(serviceRespone);
            return Ok(serviceRespone);
        }
        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrderByUserId(int userId)
        {
            var serviceRespone = await _orderReposity.GetOrderByUserId(userId);
            if (serviceRespone.Data is null)
                return NotFound(serviceRespone);
            return Ok(serviceRespone);
        }
        
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            var serviceRespone = await _orderReposity.CreateOrder(orderCreateDto);
            if (serviceRespone.Data is null)
                return NotFound(serviceRespone);
            return Ok(serviceRespone);
        }
        
        
    }
}
