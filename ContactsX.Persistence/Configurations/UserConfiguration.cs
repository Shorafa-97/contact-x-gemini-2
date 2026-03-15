using ContactsX.Domain.Entities;
using ContactsX.Domain.ValueOpjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UserName = "admin",
                Email = "admin@contactsx.com",
                PasswordHash = "AQAAAAIAAYagAAAAEGYFh3T0h1y9n6v3j7sBzqJv7s4o9LQy2jK3Yc1GqZq3m3Hk4J8x7vH9mT3cA==",
                Role = UserRole.Admin,
                Status = UserStatus.Active,
                CreatedBy = 1,
                UpdatedBy = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            }
         );
    }
}