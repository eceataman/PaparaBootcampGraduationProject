using Papara.ExpenseTrackingSystem.API.DTOs;

namespace Papara.ExpenseTrackingSystem.API.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto?> LoginAsync(UserLoginDto loginDto);
      

    }
}
