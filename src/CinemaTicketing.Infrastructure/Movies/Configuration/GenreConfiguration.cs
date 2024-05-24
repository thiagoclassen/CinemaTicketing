using CinemaTicketing.Domain.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketing.Infrastructure.Movies.Configuration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres", "movie");

        builder.HasKey(g => g.Id);

        builder.HasAlternateKey(g => g.GenreName);

        builder.Property(g => g.GenreName);
        builder.Property(g => g.Id);

        builder.HasData(Genre.ListGenres());
    }
}