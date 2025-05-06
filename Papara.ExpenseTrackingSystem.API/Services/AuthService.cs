using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Papara.ExpenseTrackingSystem.API.DTOs;
using Papara.ExpenseTrackingSystem.API.Interfaces;
using Papara.ExpenseTrackingSystem.Domain.Entities;
using Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Papara.ExpenseTrackingSystem.API.Services;

public class AuthService : IAuthService
{
    private readonly PaparaDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(PaparaDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthResultDto?> LoginAsync(UserLoginDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(u =>
            u.Email == loginDto.Email &&
            u.PasswordHash == loginDto.Password // DEMO amaçlı şifre hash yok
        );

        if (user == null)
            return null;

        var token = GenerateJwtToken(user);

        return new AuthResultDto
        {
            Token = token,
            Role = user.Role.ToString(),
            UserId = user.Id
        };
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
