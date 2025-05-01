using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.ExpenseTrackingSystem.Domain.Entities;

public class ExpenseFileConfiguration : IEntityTypeConfiguration<ExpenseFile>
{
    public void Configure(EntityTypeBuilder<ExpenseFile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FilePath).IsRequired().HasMaxLength(255);

        builder.HasOne(x => x.Expense)
               .WithMany(x => x.Files)
               .HasForeignKey(x => x.ExpenseId);
    }
}
