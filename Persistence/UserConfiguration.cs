using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.ExpenseTrackingSystem.Domain;
using Papara.ExpenseTrackingSystem.Domain.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.IBAN).HasMaxLength(26);

        builder.HasMany(x => x.Expenses)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

        builder.HasData(
            new User { Id = 1, FullName = "Admin User", Email = "admin@mail.com", PasswordHash = "hashed", IBAN = "TR000000000000000000000001", Role = Role.Admin },
            new User { Id = 2, FullName = "Personel User", Email = "personel@mail.com", PasswordHash = "hashed", IBAN = "TR000000000000000000000002", Role = Role.Personnel }
        );
    }
}
