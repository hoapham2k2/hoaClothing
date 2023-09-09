using AutoMapper;
using Microsoft.EntityFrameworkCore;
using order_service.Data;
using order_service.Dtos.OrderItemDto;
using order_service.Migrations;
using order_service.SynchnorousServiceRequest.product_service;

namespace order_service.Repositories.OrderItemRepository;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpSyncProduct _httpSyncProduct;

    public OrderItemRepository(AppDbContext context, IMapper mapper, IHttpSyncProduct httpSyncProduct)
    {
        _context = context;
        _mapper = mapper;
        _httpSyncProduct = httpSyncProduct;
    }


    public async Task<ServiceRespone<IEnumerable<OrderItemReadDto>>> GetAllOrderItemsByOrderId(int orderId)
    {
        string sqlCommand = $"SELECT * FROM order_items WHERE order_id = {orderId}";
        var orderItems = await _context.OrderItems.FromSqlRaw(sqlCommand).ToListAsync();
        var orderItemsReadDto = _mapper.Map<IEnumerable<OrderItemReadDto>>(orderItems);
        
        ServiceRespone<IEnumerable<OrderItemReadDto>> serviceRespone = new();
        serviceRespone.Data = orderItemsReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderItemReadDto>> GetOrderItemById(int id)
    {
        string sqlCommand = $"SELECT * FROM order_items WHERE id = {id}";
        var orderItem = await _context.OrderItems.FromSqlRaw(sqlCommand).FirstOrDefaultAsync();
        var orderItemReadDto = _mapper.Map<OrderItemReadDto>(orderItem);
        
        ServiceRespone<OrderItemReadDto> serviceRespone = new();
        serviceRespone.Data = orderItemReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderItemReadDto>> CreateOrderItem(OrderItemCreateDto orderItemCreateDto)
    {
        //check order exist
        if(await CheckOrderExist(orderItemCreateDto.OrderId) is false)
            throw new Exception("Order not found");
        //check product exist
        if(await _httpSyncProduct.CheckProductExist(orderItemCreateDto.ProductId) is false)
            throw new Exception("Product not found");
        
        var orderItem = _mapper.Map<OrderItem>(orderItemCreateDto);
        await _context.OrderItems.AddAsync(orderItem);
        await _context.SaveChangesAsync();
        
        ServiceRespone<OrderItemReadDto> serviceRespone = new();
        serviceRespone.Data = _mapper.Map<OrderItemReadDto>(orderItem);
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderItemReadDto>> UpdateOrderItem(int id, OrderItemCreateDto orderItemUpdateDto)
    {
        // check order item exist
        string sqlCommand = $"SELECT * FROM order_items WHERE id = {id}";
        var orderItem = await _context.OrderItems.FromSqlRaw(sqlCommand).FirstOrDefaultAsync();
        
        if (orderItem == null)
        {
            throw new Exception("Order item not found");
        }
        
        //check order exist
        if(await CheckOrderExist(orderItemUpdateDto.OrderId) is false)
            throw new Exception("Order not found");
        
        //check product exist
        if(await _httpSyncProduct.CheckProductExist(orderItemUpdateDto.ProductId) is false)
            throw new Exception("Product not found");
        
        //update order item
        orderItem = _mapper.Map(orderItemUpdateDto, orderItem);
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();
        
        ServiceRespone<OrderItemReadDto> serviceRespone = new();
        serviceRespone.Data = _mapper.Map<OrderItemReadDto>(orderItem);
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderItemReadDto>> DeleteOrderItem(int id)
    {
        // check order item exist
        string sqlCommand = $"SELECT * FROM order_items WHERE id = {id}";
        var orderItem = await _context.OrderItems.FromSqlRaw(sqlCommand).FirstOrDefaultAsync();
        
        if (orderItem == null)
        {
            throw new Exception("Order item not found");
        }
        
        //delete order item
        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
        
        ServiceRespone<OrderItemReadDto> serviceRespone = new();
        serviceRespone.Data = _mapper.Map<OrderItemReadDto>(orderItem);
        
        return serviceRespone;
    }
    
    // --> check order exist by order id
    private async Task<bool> CheckOrderExist(int orderId)
    {
        string sqlCommand = $"SELECT * FROM orders WHERE id = {orderId}";
        var order = await _context.Orders.FromSqlRaw(sqlCommand).FirstOrDefaultAsync();
        if (order == null)
        {
            return false;
        }
        return true;
    }
}