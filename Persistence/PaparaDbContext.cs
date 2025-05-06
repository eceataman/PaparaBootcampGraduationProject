using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Papara.ExpenseTrackingSystem.Domain.Entities;
using Papara.ExpenseTrackingSystem.Domain;

public class PaparaDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public PaparaDbContext(DbContextOptions<PaparaDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ExpenseFile> ExpenseFiles => Set<ExpenseFile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaparaDbContext).Assembly);

        // ✅ Default kullanıcılar seed ediliyor
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 4,
                FullName = "Admin User",
                Email = "admin@mail.com",
                PasswordHash = "admin123", // NOT: Gerçek projede hashlenmiş olmalı
                IBAN = "TR000000000000000000000001",
                Role = Role.Admin
            },
            new User
            {
                Id = 5,
                FullName = "Personel User",
                Email = "personel@mail.com",
                PasswordHash = "personel123",
                IBAN = "TR000000000000000000000002",
                Role = Role.Personnel
            }
        );
    }
}
