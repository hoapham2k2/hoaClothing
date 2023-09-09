using order_service.Dtos.OrderItemDto;
using order_service.Migrations;

namespace order_service.Repositories.OrderItemRepository;

public interface IOrderItemRepository
{
    Task<ServiceRespone<IEnumerable<OrderItemReadDto>>> GetAllOrderItemsByOrderId(int orderId);
    
    Task<ServiceRespone<OrderItemReadDto>> GetOrderItemById(int id);
    
    //create new order item
    Task<ServiceRespone<OrderItemReadDto>> CreateOrderItem(OrderItemCreateDto orderItemCreateDto);
    
    //update order item
    Task<ServiceRespone<OrderItemReadDto>> UpdateOrderItem(int id, OrderItemCreateDto orderItemUpdateDto);
    
    //delete order item
    Task<ServiceRespone<OrderItemReadDto>> DeleteOrderItem(int id);
}