using CinemaTicketing.Domain.Movies;
using CinemaTicketing.Domain.Screenings;
using CinemaTicketing.Domain.Theaters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketing.Infrastructure.Screenings.Configuration;

public class ScreeningConfiguration : IEntityTypeConfiguration<Screening>
{
    public void Configure(EntityTypeBuilder<Screening> builder)
    {
        builder.ToTable("Screenings", "screening");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();

        builder
            .HasOne<Movie>()
            .WithOne()
            .IsRequired();

        builder
            .HasOne<Room>()
            .WithMany(r => r.Screenings)
            .HasForeignKey(s => s.RoomId)
            .IsRequired();
    }
}