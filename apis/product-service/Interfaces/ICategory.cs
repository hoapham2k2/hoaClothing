using product_service.Dtos;
using product_service.Model;

namespace product_service.Interfaces;

public interface ICategory
{
    //check existing category by id or name (optional)
    Task<bool> CheckExistingCategory(int? id = null, string? name = null);
    
    //get all categories
    Task<IEnumerable<Category>> GetCategories();  //IEnumerable is a read-only collection that can be enumerated only forward, and it can’t be sorted, resided, or modified.
    
    //create category
    Task<Category> CreateCategory(CategoryCreateDto categoryCreateDto);
    
    
    
    
    
    
}