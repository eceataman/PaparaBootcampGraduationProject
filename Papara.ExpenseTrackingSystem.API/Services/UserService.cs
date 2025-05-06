using Microsoft.EntityFrameworkCore;
using Papara.ExpenseTrackingSystem.API.DTOs;
using Papara.ExpenseTrackingSystem.API.Interfaces;
using Papara.ExpenseTrackingSystem.Domain.Entities;
using Persistence;

namespace Papara.ExpenseTrackingSystem.API.Services
{
    public class UserService : IUserService
    {
        private readonly PaparaDbContext _context;

        public UserService(PaparaDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    IBAN = u.IBAN,
                    Role = u.Role.ToString()
                })
                .ToListAsync();
        }

        public async Task CreateUserAsync(UserCreateDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password, // Şifre hash'lenmeli!
                IBAN = dto.IBAN,
                Role = (Role)dto.Role
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
