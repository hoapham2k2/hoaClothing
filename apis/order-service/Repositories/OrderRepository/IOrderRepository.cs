using order_service.Dtos.OrderDto;
using order_service.Migrations;

namespace order_service.Repositories.OrderReposity;

public interface IOrderRepository
{
    //get all orders
    Task<ServiceRespone<IEnumerable<OrderReadDto>>> GetAllOrders();
    
    //get order by order id
    Task<ServiceRespone<OrderReadDto>> GetOrderByOrderId(int orderId);
    
    //get order by user id
    Task<ServiceRespone<IEnumerable<OrderReadDto>>> GetOrderByUserId(int userId);
    
    //create new order
    Task<ServiceRespone<OrderReadDto>> CreateOrder(OrderCreateDto orderCreateDto);
    
    
}