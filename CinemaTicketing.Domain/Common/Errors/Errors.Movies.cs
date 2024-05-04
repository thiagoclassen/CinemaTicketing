using ErrorOr;

namespace CinemaTicketing.Domain.Common.Errors;

public static class Errors
{
    public static class Movies
    {
        public static Error InvalidId => Error.NotFound(
            "Movie.InvalidId",
            "Invalid Movie Id."
        );
    }
}