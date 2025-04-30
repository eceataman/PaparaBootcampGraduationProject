using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Papara.ExpenseTrackingSystem.Domain.Entities;
using System.Reflection.Emit;

public class PaparaDbContext : DbContext
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
    }
}
