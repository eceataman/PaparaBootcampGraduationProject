using Microsoft.EntityFrameworkCore;
using Papara.ExpenseTrackingSystem.API.DTOs;
using Papara.ExpenseTrackingSystem.API.Interfaces;
using Papara.ExpenseTrackingSystem.Domain.Entities;
using Persistence;

namespace Papara.ExpenseTrackingSystem.API.Services;

public class CategoryService : ICategoryService
{
    private readonly PaparaDbContext _context;

    public CategoryService(PaparaDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        return await _context.Categories
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task CreateAsync(CategoryDto dto)
    {
        var newCategory = new Category { Name = dto.Name };
        await _context.Categories.AddAsync(newCategory);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, CategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null) return;

        category.Name = dto.Name;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Expenses)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null) return false;
        if (category.Expenses.Any()) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
