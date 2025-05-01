using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.ExpenseTrackingSystem.Domain.Entities;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(x => x.Expenses)
               .WithOne(x => x.Category)
               .HasForeignKey(x => x.CategoryId);

        builder.HasData(
            new Category { Id = 1, Name = "Yemek" },
            new Category { Id = 2, Name = "Ulaşım" }
        );
    }
}
