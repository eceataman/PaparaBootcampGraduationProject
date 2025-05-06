using Papara.ExpenseTrackingSystem.API.DTOs;

namespace Papara.ExpenseTrackingSystem.API.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserCreateDto dto);
        Task<List<UserDto>> GetAllUsersAsync();
        Task DeleteUserAsync(int id);
    }
}
