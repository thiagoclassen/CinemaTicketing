using CinemaTicketing.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketing.Infrastructure.Users.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "user");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();
    }
}