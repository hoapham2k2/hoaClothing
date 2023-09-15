using product_service.Dtos;
using product_service.Model;

namespace product_service.Repositories.ProductImageRepository;

public interface IProductImageRepository
{
    //get all product images by product id
    Task<IEnumerable<ImageUriReadDto>> GetProductImagesByProductId(int productId);
}