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
    
    public async Task<ServiceResponse<IEnumerable<ProductReadDto>>> GetProducts()
    {
        string sqlCommand = "SELECT * FROM Products";
        var products = await _context.Products.FromSqlRaw(sqlCommand).ToListAsync();
        var productReadDtos = _mapper.Map<IEnumerable<ProductReadDto>>(products);
        return new ServiceResponse<IEnumerable<ProductReadDto>>
        {
            Data = productReadDtos
        };
    }

    public async Task<ServiceResponse<ProductReadDto>> GetProductById(int id)
    {
        string sqlCommand = "SELECT * FROM Products WHERE Id = {0}";
        var product = await _context.Products.FromSqlRaw(sqlCommand, id).FirstOrDefaultAsync();
        var productReadDto = _mapper.Map<ProductReadDto>(product);
        return new ServiceResponse<ProductReadDto>
        {
            Data = productReadDto
        };
        
    }

    public async Task<ServiceResponse<IEnumerable<ProductReadDto>>> GetProductByCategoryId(int categoryId)
    {
        string sqlCommand = "SELECT * FROM Products WHERE CategoryId = {0}";
        var products = await _context.Products.FromSqlRaw(sqlCommand, categoryId).ToListAsync();
        var productReadDtos = _mapper.Map<IEnumerable<ProductReadDto>>(products);
        return new ServiceResponse<IEnumerable<ProductReadDto>>
        {
            Data = productReadDtos
        };
        
    }

    public async Task<ServiceResponse<ProductReadDto>> CreateProduct(ProductCreateDto productCreateDto)
    {
        var product = _mapper.Map<Product>(productCreateDto);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        var productReadDto = _mapper.Map<ProductReadDto>(product);
        return new ServiceResponse<ProductReadDto>
        {
            Data = productReadDto
        };
    }

    public async Task<ServiceResponse<IEnumerable<ProductReadDto>>> GetProducts(int limit)
    {
        // sử dụng sql server để pagination
        string sqlCommand = "SELECT * FROM Products ORDER BY Id OFFSET 0 ROWS FETCH NEXT {0} ROWS ONLY";
        var products = await _context.Products.FromSqlRaw(sqlCommand, limit).ToListAsync();
        var productReadDtos = _mapper.Map<IEnumerable<ProductReadDto>>(products);
        return new ServiceResponse<IEnumerable<ProductReadDto>>
        {
            Data = productReadDtos
        };
    }

    public async Task<ServiceResponse<ProductReadDto>> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
    {
        // check if product exist by id
        if(!await CheckExistingProduct(id))
            return new ServiceResponse<ProductReadDto>
            {
                Messages = new List<string> {"Product not found"},
            };
        // check if product exist by name
        if(await CheckExistingProduct(productUpdateDto.Name))
            return new ServiceResponse<ProductReadDto>
            {
                Messages = new List<string> {"Product name already exist"},
            };
        
        // mapping productUpdateDto to product
        var product = _mapper.Map<Product>(productUpdateDto);
        
        // update product
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        
        // mapping product to productReadDto
        var productReadDto = _mapper.Map<ProductReadDto>(product);
        return new ServiceResponse<ProductReadDto>
        {
            Data = productReadDto
        };
    }

    public async Task<ServiceResponse<ProductReadDto>> DeleteProduct(int id)
    {
        string sqlCommand = "SELECT * FROM Products WHERE Id = {0}";
        var product = await _context.Products.FromSqlRaw(sqlCommand, id).FirstOrDefaultAsync();
        if (product == null)
            return new ServiceResponse<ProductReadDto>
            {
                Messages = new List<string> {"Product not found"},
            };
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        var productReadDto = _mapper.Map<ProductReadDto>(product);
        
        return new ServiceResponse<ProductReadDto>
        {
            Data = productReadDto
        };
        
    }

    
    


    //------------------private method------------------
    // use Generic to check existing product by id or name
    public async Task<bool> CheckExistingProduct<T>(T value)
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