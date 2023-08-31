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
    
    public async Task<bool> CheckExistingCategory(int? categoryId = null, string? categoryName = null)
    {
        if(categoryId == 0 && categoryName == null)
            throw new ArgumentNullException(nameof(categoryId));
        if(categoryId != null)
            return await _context.Categories.AnyAsync(x => x.Id == categoryId);
        return await _context.Categories.AnyAsync(x => x.Name == categoryName);
    }
   
    
    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }
    
    public async Task<Category> CreateCategory(CategoryCreateDto categoryCreateDto)
    {
        if(await CheckExistingCategory(null,categoryCreateDto.Name) is true)
            throw new ArgumentNullException(nameof(categoryCreateDto.Name));
        var category = _mapper.Map<Category>(categoryCreateDto); // tiến hành mapping từ categoryCreateDto sang category (đã được cấu hình trong MappingProfile.cs) 
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
        
    }
    
    
    
    
}