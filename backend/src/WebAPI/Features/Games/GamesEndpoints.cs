using GameNotes.WebAPI.Entities;
using GameNotes.WebAPI.Features.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GameNotes.WebAPI.Features.Games;

public static class GamesEndpoints
{
    public static void AddGamesEndpoints(this WebApplication app)
    {
        app.MapGet("/games", GetGamesPage);
    }

    private static async Task<Results<Ok<PaginatedList<Game>>, ProblemHttpResult>> GetGamesPage(
        [AsParameters] GamesPageRequest request,
        [FromServices] GamesSourceService gamesService)
    {
        var result = await gamesService.GetGamesPageAsync(request.Page, request.PageSize, request.Search);
        
        return TypedResults.Ok(result);
    }
}