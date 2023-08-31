using AutoMapper;
using product_service.Dtos;
using product_service.Model;

namespace product_service.MappingProfiles;

public class MappingProfile : Profile
{
 // tiến hành cấu hình mapping cho các model và dto

 //mapping cho Category
 public MappingProfile()
 {
  CreateMap<Category, CategoryReadDto>();
  CreateMap<CategoryCreateDto, Category>();
  
 }

}