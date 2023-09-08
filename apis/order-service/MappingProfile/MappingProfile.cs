using AutoMapper;
using order_service.Dtos.OrderDto;
using order_service.Dtos.OrderItemDto;
using order_service.Migrations;

namespace order_service.MappingProfile;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<OrderCreateDto, Order>();
        CreateMap<Order, OrderReadDto>();
        
        CreateMap<OrderItemCreateDto, OrderItem>();
        CreateMap<OrderItem, OrderItemReadDto>();
        
    }
}