using AutoMapper;
using Microsoft.EntityFrameworkCore;
using product_service.Data;
using product_service.Dtos;
using product_service.Model;

namespace product_service.Repositories.ProductImageRepository;

public class ProductImageRepository : IProductImageRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public ProductImageRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ImageUriReadDto>> GetProductImagesByProductId(int productId)
    {
        string sqlCommand = "SELECT * FROM ImageUris WHERE ProductId = {0}";
        var imageUris = await _dbContext.ImageUris.FromSqlRaw(sqlCommand, productId).ToListAsync();
        
        var imageUriReadDtos = _mapper.Map<IEnumerable<ImageUriReadDto>>(imageUris);
        
        return imageUriReadDtos;
    }
}