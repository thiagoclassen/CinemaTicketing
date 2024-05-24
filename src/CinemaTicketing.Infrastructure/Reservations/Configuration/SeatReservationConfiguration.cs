using CinemaTicketing.Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketing.Infrastructure.Reservations.Configuration;

public class SeatReservationConfiguration : IEntityTypeConfiguration<SeatReservation>
{
    public void Configure(EntityTypeBuilder<SeatReservation> builder)
    {
        builder.ToTable("SeatReservations", "reservation");

        builder.HasKey(sr => sr.Id);
        builder.Property(sr => sr.Id).ValueGeneratedNever();

        builder
            .HasOne(sr => sr.Seat)
            .WithMany()
            .HasForeignKey(sr => sr.SeatId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}