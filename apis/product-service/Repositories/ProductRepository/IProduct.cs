using product_service.Dtos;
using product_service.Model;

namespace product_service.Interfaces;

public interface IProduct
{
    //get all products
    Task<ServiceResponse<IEnumerable<ProductReadDto>>> GetProducts();
    
    //get product by id
    Task<ServiceResponse<ProductReadDto>> GetProductById(int id);
    
    //get product by category id
    Task<ServiceResponse<IEnumerable<ProductReadDto>>> GetProductByCategoryId(int categoryId);
    
    
    //create product
    Task<ServiceResponse<ProductReadDto>> CreateProduct(ProductCreateDto productCreateDto);
    
    //limit result
    Task<ServiceResponse<IEnumerable<ProductReadDto>>> GetProducts(int limit);
    
    //update product
    Task<ServiceResponse<ProductReadDto>> UpdateProduct(int id, ProductUpdateDto productUpdateDto);
    
    //delete product
    Task<ServiceResponse<ProductReadDto>> DeleteProduct(int id);
    
    
    // ----------> helper methods <----------
    //check existing product
    Task<bool> CheckExistingProduct<T>(T value);
}