using product_service.Dtos;
using product_service.Model;

namespace product_service.Interfaces;

public interface IProduct
{
    //get all products
    Task<IEnumerable<Product>> GetProducts();
    
    //get product by id
    Task<Product?> GetProductById(int id);
    
    //get product by category id
    Task<IEnumerable<Product>> GetProductByCategoryId(int categoryId);
    
    
    //create product
    Task<Product> CreateProduct(ProductCreateDto productCreateDto);
    
    //limit result
    Task<IEnumerable<Product>> GetProducts(int limit);
    
    //update product
    Task<Product> UpdateProduct(int id, ProductUpdateDto productUpdateDto);
    
    // ----------> helper methods <----------
    //check existing product
    Task<bool> CheckExistingProduct<T>(T value);
}