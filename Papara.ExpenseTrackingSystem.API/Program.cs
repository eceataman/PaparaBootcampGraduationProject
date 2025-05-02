using Microsoft.EntityFrameworkCore;
using Papara.ExpenseTrackingSystem.API.Interfaces;
using Papara.ExpenseTrackingSystem.API.Services;
using Persistence; // PaparaDbContext buradaysa
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ?? Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ?? Swagger config (opsiyonel geliþtirme)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Papara Expense Tracking API",
        Version = "v1"
    });
});

// ?? EF Core – DbContext
builder.Services.AddDbContext<PaparaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ?? Dependency Injection – Service registrations
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
// builder.Services.AddScoped<ICategoryService, CategoryService>(); // varsa eklersin
// builder.Services.AddScoped<IUserService, UserService>();         // varsa eklersin

// ?? CORS – Ýleride frontend baðlamak için
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ?? Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
