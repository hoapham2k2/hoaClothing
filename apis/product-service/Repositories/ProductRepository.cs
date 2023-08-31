using AutoMapper;
using Microsoft.EntityFrameworkCore;
using product_service.Data;
using product_service.Interfaces;
using product_service.Model;

namespace product_service.Repositories;

public class ProductRepository : IProduct
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICategory _categoryRepository;
    
    public ProductRepository(AppDbContext context, IMapper mapper, ICategory categoryRepository)
    {
        _context = context;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<bool> CheckExistingProduct(int? id = null, string? name = null)
    {
        if(id is not null)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product is not null;
        }
        if(name is not null)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == name);
            return product is not null;
        }
        return false;
    }
   

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductById(int id)
    {
        if(await CheckExistingProduct(id, null) is false)
            throw new ArgumentNullException(nameof(id) + " does not exist");
        return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductByCategoryId(int categoryId)
    {
        if(await _categoryRepository.CheckExistingCategory(categoryId, null) is false)
            throw new ArgumentNullException(nameof(categoryId) + " does not exist");
        return await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
    }
}