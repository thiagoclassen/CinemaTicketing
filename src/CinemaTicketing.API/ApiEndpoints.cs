namespace CinemaTicketing.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Movies
    {
        private const string Base = $"{ApiBase}/movies";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:int}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:int}}";
        public const string Delete = $"{Base}/{{id:int}}";
    }

    public static class Genres
    {
        private const string Base = $"{ApiBase}/genres";

        public const string Get = $"{Base}/{{id:int}}";
        public const string GetAll = Base;
    }

    public static class Errors
    {
        public const string Base = "/error";
    }
}