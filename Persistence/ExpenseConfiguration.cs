using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.ExpenseTrackingSystem.Domain.Entities;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.PaymentTool).HasMaxLength(50);
        builder.Property(x => x.Location).HasMaxLength(100);
        builder.Property(x => x.RejectionReason).HasMaxLength(255);
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne(x => x.User)
               .WithMany(x => x.Expenses)
               .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Category)
               .WithMany(x => x.Expenses)
               .HasForeignKey(x => x.CategoryId);
    }
}
