using product_service.Model;

namespace product_service.Interfaces;

public interface IProduct
{
    //check if product exists by id or name (optional)
    Task<bool> CheckExistingProduct(int? id = null, string? name = null);
    
    //get all products
    Task<IEnumerable<Product>> GetProducts();
    
    //get product by id
    Task<Product?> GetProductById(int id);
    
    //get product by category id
    Task<IEnumerable<Product>> GetProductByCategoryId(int categoryId);
    
    
}