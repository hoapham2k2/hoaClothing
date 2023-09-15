using product_service.Dtos;
using product_service.Model;

namespace product_service.Interfaces;

public interface ICategory
{
    Task<bool> CheckExistingCategory<T>(T value);
    Task<IEnumerable<Category>> GetCategories();  
    Task<Category> CreateCategory(CategoryCreateDto categoryCreateDto);
    
    //get category by id
    Task<ServiceResponse<CategoryReadDto>> GetCategoryById(int id);
    
}