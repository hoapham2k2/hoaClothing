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
    private IOrderItemRepository _orderItemRepositoryImplementation;

    public OrderItemRepository(AppDbContext context, IMapper mapper, IHttpSyncProduct httpSyncProduct)
    {
        _context = context;
        _mapper = mapper;
        _httpSyncProduct = httpSyncProduct;
    }


    public async Task<ServiceRespone<IEnumerable<OrderItemReadDto>>> GetAllOrderItemsByOrderId(int orderId)
    {
        //check if order id is valid
        if (await CheckExistingOrderItem(orderId) is false)
            throw new ArgumentNullException(nameof(orderId) + " does not exist");
        string sqlCommand = $"SELECT * FROM order_items WHERE order_id = {orderId}";
        var orderItems = await _context.OrderItems.FromSqlRaw(sqlCommand).ToListAsync();
        var orderItemsReadDto = _mapper.Map<IEnumerable<OrderItemReadDto>>(orderItems);
        
        var serviceRespone = new ServiceRespone<IEnumerable<OrderItemReadDto>>();
        serviceRespone.Data = orderItemsReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderItemReadDto>> GetOrderItemById(int id)
    {
        //check if order item id is valid
        if (await CheckExistingOrderItem(id) is false)
            throw new ArgumentNullException(nameof(id) + " does not exist");
        string sqlCommand = $"SELECT * FROM order_items WHERE id = {id}";
        var orderItem = await _context.OrderItems.FromSqlRaw(sqlCommand).FirstOrDefaultAsync();
        var orderItemReadDto = _mapper.Map<OrderItemReadDto>(orderItem);
        
        var serviceRespone = new ServiceRespone<OrderItemReadDto>();
        serviceRespone.Data = orderItemReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderItemReadDto>> CreateOrderItem(OrderItemCreateDto orderItemCreateDto)
    {
        //check if order id is valid
        if (await CheckExistingOrderItem(orderItemCreateDto.OrderId) is false)
            throw new ArgumentNullException(nameof(orderItemCreateDto.OrderId) + " does not exist");
        //check if product id is valid
        if (await _httpSyncProduct.CheckProductExist(orderItemCreateDto.ProductId) is false)
            throw new ArgumentNullException(nameof(orderItemCreateDto.ProductId) + " does not exist");
        //check if order item already exists
        if (await CheckExistingOrderItem(orderItemCreateDto.OrderId, orderItemCreateDto.ProductId) is true)
            throw new ArgumentException(nameof(orderItemCreateDto) + " already exists");
        
        var orderItem = _mapper.Map<OrderItem>(orderItemCreateDto);
        await _context.OrderItems.AddAsync(orderItem);
        await _context.SaveChangesAsync();
        var orderItemReadDto = _mapper.Map<OrderItemReadDto>(orderItem);
        
        var serviceRespone = new ServiceRespone<OrderItemReadDto>();
        serviceRespone.Data = orderItemReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderItemReadDto>> UpdateOrderItem(int id, OrderItemCreateDto orderItemUpdateDto)
    {
        //check if order item id is valid
        if (await CheckExistingOrderItem(id) is false)
            throw new ArgumentNullException(nameof(id) + " does not exist");
        //check if order id is valid
        if (await CheckExistingOrderItem(orderItemUpdateDto.OrderId) is false)
            throw new ArgumentNullException(nameof(orderItemUpdateDto.OrderId) + " does not exist");
        //check if product id is valid
        if (await _httpSyncProduct.CheckProductExist(orderItemUpdateDto.ProductId) is false)
            throw new ArgumentNullException(nameof(orderItemUpdateDto.ProductId) + " does not exist");
        //check if order item already exists
        if (await CheckExistingOrderItem(orderItemUpdateDto.OrderId, orderItemUpdateDto.ProductId) is true)
            throw new ArgumentException(nameof(orderItemUpdateDto) + " already exists");
        
        var orderItem = _mapper.Map<OrderItem>(orderItemUpdateDto);
        orderItem.Id = id;
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();
        var orderItemReadDto = _mapper.Map<OrderItemReadDto>(orderItem);
        
        var serviceRespone = new ServiceRespone<OrderItemReadDto>();
        serviceRespone.Data = orderItemReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderItemReadDto>> DeleteOrderItem(int id)
    {
        //check if order item id is valid
        if (await CheckExistingOrderItem(id) is false)
            throw new ArgumentNullException(nameof(id) + " does not exist");
        
        // delete order item
        string sqlCommand = $"DELETE FROM order_items WHERE id = {id}";
        await _context.Database.ExecuteSqlRawAsync(sqlCommand);
        
        var serviceRespone = new ServiceRespone<OrderItemReadDto>();
        serviceRespone.Data = null;
        
        return serviceRespone;
    }
    
    
    //helper methods
    private async Task<bool> CheckExistingOrderItem(int id)
    {
        string sqlCommand = $"SELECT * FROM order_items WHERE id = {id}";
        var orderItem = await _context.OrderItems.FromSqlRaw(sqlCommand).FirstOrDefaultAsync();
        return orderItem is not null;
    }
    
    private async Task<bool> CheckExistingOrderItem(int orderId, int productId)
    {
        string sqlCommand = $"SELECT * FROM order_items WHERE order_id = {orderId} AND product_id = {productId}";
        var orderItem = await _context.OrderItems.FromSqlRaw(sqlCommand).FirstOrDefaultAsync();
        return orderItem is not null;
    }
    
}