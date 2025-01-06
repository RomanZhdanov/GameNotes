using GameNotes.WebAPI.Entities;
using GameNotes.WebAPI.Features.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GameNotes.WebAPI.Features.Games;

public static class GamesEndpoints
{
    public static void AddGamesEndpoints(this WebApplication app)
    {
        app.MapGet("/games/search", GetGames);
    }

    private static async Task<Results<Ok<PaginatedList<Game>>, ProblemHttpResult>> GetGames(
        [FromQuery] int page, int pageSize, string input,
        [FromServices] GamesSourceService gamesService)
    {
        var result = await gamesService.SearchGamesAsync(page, pageSize, input);
        
        return TypedResults.Ok(result);
    }
}