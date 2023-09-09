using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using order_service.Dtos.OrderItemDto;
using order_service.Repositories.OrderItemRepository;

namespace order_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        
        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
        
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetAllOrderItemsByOrderId(int orderId)
        {
            var serviceRespone = await _orderItemRepository.GetAllOrderItemsByOrderId(orderId);
            if (serviceRespone.Data is null)
                return NotFound(serviceRespone);
            return Ok(serviceRespone);
        }
        
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var serviceRespone = await _orderItemRepository.GetOrderItemById(id);
            if (serviceRespone.Data is null)
                return NotFound(serviceRespone);
            return Ok(serviceRespone);
        }
        
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemCreateDto orderItemCreateDto)
        {
            var serviceRespone = await _orderItemRepository.CreateOrderItem(orderItemCreateDto);
            if (serviceRespone.Data is null)
                return NotFound(serviceRespone);
            return Ok(serviceRespone);
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemCreateDto orderItemUpdateDto)
        {
            var serviceRespone = await _orderItemRepository.UpdateOrderItem(id, orderItemUpdateDto);
            if (serviceRespone.Data is null)
                return NotFound(serviceRespone);
            return Ok(serviceRespone);
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var serviceRespone = await _orderItemRepository.DeleteOrderItem(id);
            if (serviceRespone.Data is null)
                return NotFound(serviceRespone);
            return Ok(serviceRespone);
        }
        
        
    }
}
