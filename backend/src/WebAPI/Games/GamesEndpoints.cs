using Microsoft.AspNetCore.Http.HttpResults;

namespace GameNotes.WebAPI.Games;

public static class GamesEndpoints
{
    public static void AddGamesEndpoints(this WebApplication app)
    {
        app.MapGet("/games", GetGames);
    }

    public static async Task<Results<Ok<GamesResult>, ProblemHttpResult>> GetGames()
    {
        var result = new GamesResult()
        {
            Id = 1,
            Name = "Mario Odyssey"
        };

        return TypedResults.Ok(result);
    }
}