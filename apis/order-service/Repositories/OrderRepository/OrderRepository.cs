﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using order_service.Data;
using order_service.Dtos.OrderDto;
using order_service.Migrations;
using order_service.SynchnorousServiceRequest.auth_service;
using order_service.SynchnorousServiceRequest.product_service;

namespace order_service.Repositories.OrderReposity;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpSyncAuth _httpSyncAuth;
 
    public OrderRepository(AppDbContext context, IMapper mapper, IHttpSyncAuth httpSyncAuth)
    {
        _context = context;
        _mapper = mapper;
        _httpSyncAuth = httpSyncAuth;
    }

    public async Task<ServiceRespone<IEnumerable<OrderReadDto>>> GetAllOrders()
    {
        string sqlCommand = "SELECT * FROM orders";
        var orders = await _context.Orders.FromSqlRaw(sqlCommand).ToListAsync();
        var ordersReadDto = _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        
        ServiceRespone<IEnumerable<OrderReadDto>> serviceRespone = new();
        serviceRespone.Data = ordersReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderReadDto>> GetOrderByOrderId(int orderId)
    {
        string sqlCommand = $"SELECT * FROM orders WHERE id = {orderId}";
        var order = await _context.Orders.FromSqlRaw(sqlCommand).FirstOrDefaultAsync();
        var orderReadDto = _mapper.Map<OrderReadDto>(order);
        
        ServiceRespone<OrderReadDto> serviceRespone = new();
        serviceRespone.Data = orderReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<IEnumerable<OrderReadDto>>> GetOrderByUserId(int userId)
    {
        if(await CheckUserExist(userId) == false)
            throw new Exception("User not exist");
        
        string sqlCommand = $"SELECT * FROM orders WHERE UserId = {userId}";
        var orders = await _context.Orders.FromSqlRaw(sqlCommand).ToListAsync();
        var ordersReadDto = _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        
        ServiceRespone<IEnumerable<OrderReadDto>> serviceRespone = new();
        serviceRespone.Data = ordersReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderReadDto>> GetOrderById(int id)
    {
        string sqlCommand = $"SELECT * FROM orders WHERE id = {id}";
        var order = await _context.Orders.FromSqlRaw(sqlCommand).FirstOrDefaultAsync();
        var orderReadDto = _mapper.Map<OrderReadDto>(order);
        
        ServiceRespone<OrderReadDto> serviceRespone = new();
        serviceRespone.Data = orderReadDto;
        
        return serviceRespone;
    }

    public async Task<ServiceRespone<OrderReadDto>> CreateOrder(OrderCreateDto orderCreateDto)
    {
        var order = _mapper.Map<Order>(orderCreateDto);
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        
        ServiceRespone<OrderReadDto> serviceRespone = new();
        serviceRespone.Data = _mapper.Map<OrderReadDto>(order);
        
        return serviceRespone;
    }
    
    
    // --> helper function
    
    // check user exist
    private async Task<bool> CheckUserExist(int userId)
    {
        return await _httpSyncAuth.CheckUserExist(userId);
    }
    
}