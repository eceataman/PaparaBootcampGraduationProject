using Microsoft.EntityFrameworkCore;
using Papara.ExpenseTrackingSystem.API.Interfaces;
using Papara.ExpenseTrackingSystem.API.Services;
using Persistence; // PaparaDbContext buradaysa
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ? Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ? Swagger config
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Papara Expense Tracking API",
        Version = "v1"
    });
});

// ? EF Core � DbContext
builder.Services.AddDbContext<PaparaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ? Dependency Injection � Service registrations
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddScoped<IUserService, UserService>(); // varsa eklersin

// ? CORS � �leride frontend ba�lamak i�in
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ? Authentication ve Authorization � app.Build()'dan �nce
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = builder.Configuration["Jwt:Key"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
    };
});

builder.Services.AddAuthorization();

// ? app yap�land�rmas�
var app = builder.Build();

// ? Swagger UI sadece development ortam�nda �al���r
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// ? Bu s�raya dikkat!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
