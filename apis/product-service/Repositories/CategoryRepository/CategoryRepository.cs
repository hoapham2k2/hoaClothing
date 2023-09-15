using AutoMapper;
using Microsoft.EntityFrameworkCore;
using product_service.Data;
using product_service.Dtos;
using product_service.Interfaces;
using product_service.Model;

namespace product_service.Repositories;

public class CategoryRepository : ICategory
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    
    public CategoryRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<bool> CheckExistingCategory<T>(T value)
    {
       if (value == null)
           throw new ArgumentNullException(nameof(value));
       switch (value)
       {
           case int:
           {
               var id = (int)(object)value;
               return await _context.Categories.AnyAsync(x => x.Id == id);
           }
           case string:
           {
                var name = (string)(object)value;
                return await _context.Categories.AnyAsync(x => x.Name == name);
           }
           default:
               throw new ArgumentException("Invalid type");
       }
       
    }
   
    
    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }
    
    public async Task<Category> CreateCategory(CategoryCreateDto categoryCreateDto)
    {
        if(await CheckExistingCategory(categoryCreateDto.Name) is true)
            throw new ArgumentException(nameof(categoryCreateDto.Name) + " already exists");
        var category = _mapper.Map<Category>(categoryCreateDto);
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<ServiceResponse<CategoryReadDto>> GetCategoryById(int id)
    {
        var sqlCommand = "SELECT * FROM Categories WHERE Id = {0}";
        var category = await _context.Categories.FromSqlRaw(sqlCommand, id).FirstOrDefaultAsync();
        if (category == null)
            return new ServiceResponse<CategoryReadDto>
            {
                Messages = new List<string> {"Category not found"},
            };
        var categoryReadDto = _mapper.Map<CategoryReadDto>(category);
        return new ServiceResponse<CategoryReadDto>
        {
            Data = categoryReadDto
        };
        
    }
}