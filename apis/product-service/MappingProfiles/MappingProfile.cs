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
  
  //mapping cho Product
  CreateMap<Product, ProductReadDto>();
  CreateMap<ProductCreateDto, Product>();
  CreateMap<ProductUpdateDto, Product>();
  
  //mapping cho product image uri
  CreateMap<ImageUri, ImageUriReadDto>();
  CreateMap<ImageUriCreateDto, ImageUri>();
 }

}