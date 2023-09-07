using AutoMapper;
using Microsoft.EntityFrameworkCore;
using product_service.Data;
using product_service.Dtos;
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

    public async Task<IEnumerable<Product>> GetProducts()
    {
        string sqlCommand = "Select * from Products";
        return await _context.Products.FromSqlRaw(sqlCommand).ToListAsync();
    }

    public async Task<Product?> GetProductById(int id)
    {
        if (await CheckExistingProduct(id) is false)
            throw new ArgumentNullException(nameof(id) + " does not exist");
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetProductByCategoryId(int categoryId)
    {
        if (await _categoryRepository.CheckExistingCategory(categoryId) is false)
            throw new ArgumentNullException(nameof(categoryId) + " does not exist");
        string sqlCommand = $"Select * from Products where CategoryId = {categoryId}";
        return await _context.Products.FromSqlRaw(sqlCommand).ToListAsync();
    }

    public async Task<Product> CreateProduct(ProductCreateDto productCreateDto)
    {
        //check only category id is valid
        if (await _categoryRepository.CheckExistingCategory(productCreateDto.CategoryId) is false)
            throw new ArgumentNullException(nameof(productCreateDto.CategoryId) + " does not exist");
        //check product name is unique
        if (await CheckExistingProduct(productCreateDto.Name) is true)
            throw new ArgumentException(nameof(productCreateDto.Name) + " already exists");
        
        var product = _mapper.Map<Product>(productCreateDto);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
        
    }

    public async Task<IEnumerable<Product>> GetProducts(int limit)
    {
        //check limit is valid
        if (limit <= 0)
            throw new ArgumentException(nameof(limit) + " must be greater than 0");
        string sqlCommand = $"Select * from Products limit {limit}";
        return await _context.Products.FromSqlRaw(sqlCommand).ToListAsync();
    }

    public async Task<Product> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
    {
        //check product id is valid
        if (await CheckExistingProduct(id) is false)
            throw new ArgumentNullException(nameof(id) + " does not exist");
        //check category id is valid
        if (await _categoryRepository.CheckExistingCategory(productUpdateDto.CategoryId) is false)
            throw new ArgumentNullException(nameof(productUpdateDto.CategoryId) + " does not exist");
        //check product name is unique
        if (await CheckExistingProduct(productUpdateDto.Name) is true)
            throw new ArgumentException(nameof(productUpdateDto.Name) + " already exists");
        
        var product = await _context.Products.FindAsync(id);
        
        //update product
        var productUpdate = _mapper.Map(productUpdateDto, product);
        _context.Products.Update(productUpdate ?? throw new InvalidOperationException());
        await _context.SaveChangesAsync();
        
        return productUpdate;
    }


    //------------------private method------------------
    // use Generic to check existing product by id or name
    private async Task<bool> CheckExistingProduct<T>(T value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));
        switch (value)
        {
            case int:
            {
                var id = (int)(object)value;
                return await _context.Products.AnyAsync(x => x.Id == id);
            }
            case string:
            {
                var name = (string)(object)value;
                return await _context.Products.AnyAsync(x => x.Name == name);
            }
            default:
                throw new ArgumentException(nameof(value) + " is not a valid type");
        }
    }
}