using CinemaTicketing.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketing.Infrastructure.Reservations.Configuration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations", "reservation");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();

        builder
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId)
            .IsRequired();

        builder
            .HasOne(r => r.Screening)
            .WithMany()
            .HasForeignKey(r => r.ScreeningId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(r => r.SeatReservations)
            .WithOne(sr => sr.Reservation)
            .HasForeignKey(sr => sr.ReservationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}