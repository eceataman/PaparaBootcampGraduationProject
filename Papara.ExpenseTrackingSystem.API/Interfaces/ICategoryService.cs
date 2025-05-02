using Papara.ExpenseTrackingSystem.API.DTOs;

namespace Papara.ExpenseTrackingSystem.API.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task CreateAsync(CategoryDto dto);
        Task UpdateAsync(int id, CategoryDto dto);
        Task<bool> DeleteAsync(int id); // true: silindi, false: aktif masraf var
    }
}
