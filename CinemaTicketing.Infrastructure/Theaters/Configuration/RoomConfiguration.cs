using CinemaTicketing.Domain.Screenings;
using CinemaTicketing.Domain.Theaters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketing.Infrastructure.Theaters.Configuration;

public class RoomConfiguration:IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms", "theater");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();

        builder
            .HasOne<Theater>()
            .WithMany(t=>t.Rooms)
            .HasForeignKey(r => r.TheaterId)
            .IsRequired();
        
        builder
            .HasMany(r => r.Seats)
            .WithOne()
            .HasForeignKey(s=> s.RoomId)
            .IsRequired();
    }
}