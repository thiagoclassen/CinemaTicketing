// ReSharper disable UnusedMember.Local
namespace CinemaTicketing.Domain.Movies;

public sealed class Genre
{
    public Genre()
    {
    }

    public Genre(string genre)
    {
        Enum.TryParse<AvailableGenres>(genre, true, out var genreEnum);

        Id = (int)genreEnum;
        GenreName = Enum.GetName(typeof(AvailableGenres), genreEnum)!;
    }

    public int Id { get; private set; }
    public string GenreName { get; private set; } = null!;

    public static IEnumerable<Genre> ListGenres()
    {
        return Enum.GetNames(typeof(AvailableGenres)).Select(genre => new Genre(genre));
    }

    private enum AvailableGenres
    {
        Action = 1,
        Adventure = 2,
        Comedy = 3,
        Drama = 4,
        Fantasy = 5,
        Horror = 6,
        Mystery = 7,
        Romance = 8,
        Thriller = 9,
        SciFi = 10,
        Western = 11,
        Animation = 12,
        Crime = 13,
        Documentary = 14,
        Family = 15,
        History = 16,
        Music = 17,
        War = 18,
        Sport = 19,
        Biography = 20,
        Musical = 21
    }
}