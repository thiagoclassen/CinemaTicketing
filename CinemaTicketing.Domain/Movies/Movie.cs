using System.Text.RegularExpressions;

namespace CinemaTicketing.Domain.Movies;

public sealed partial class Movie
{
    private string _slug = string.Empty;
    public int Id { get; init; }
    public required string Title { get; set; }
    public required int YearOfRelease { get; set; }

    public string Slug
    {
        get => GenerateSlug();
        init => _slug = value;
    }

    public required string Description { get; set; }
    public required string Director { get; set; }
    public required int Duration { get; set; }
    public required int AgeRestriction { get; set; }

    public List<Genre> Genres { get; set; } = [];

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