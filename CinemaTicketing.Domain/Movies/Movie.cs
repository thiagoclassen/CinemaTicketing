using System.Text.RegularExpressions;

namespace CinemaTicketing.Domain.Movies;

public partial class Movie
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required int YearOfRelease { get; init; }
    public string Slug => GenerateSlug();
    public required string Description { get; init; }
    public required string Director { get; init; }
    public required int Duration { get; init; }
    public required int AgeRestriction { get; init; }

    public List<Genre> Genres { get; init; } = [];

    private string GenerateSlug()
    {
        var sluggedTitle = SlugRegex().Replace(Title, string.Empty)
            .ToLower()
            .Replace(" ", "-");
        return $"{sluggedTitle}-{YearOfRelease}";
    }

    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
    private static partial Regex SlugRegex();
}