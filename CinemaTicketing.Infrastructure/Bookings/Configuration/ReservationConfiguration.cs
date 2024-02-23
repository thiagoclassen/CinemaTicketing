using CinemaTicketing.Domain;
using CinemaTicketing.Domain.Booking;
using CinemaTicketing.Domain.Theaters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketing.Infrastructure.Bookings.Configuration;

public class ReservationConfiguration:IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations", "booking");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();

        builder
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId)
            .IsRequired();
        
        builder
            .HasMany(r => r.Seats)
            .WithOne()
            .HasForeignKey("ReservationId");
    }
}