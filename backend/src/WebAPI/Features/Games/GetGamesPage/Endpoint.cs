using GameNotes.WebAPI.Entities;
using GameNotes.WebAPI.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GameNotes.WebAPI.Features.Games.GetGamesPage;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/games", Handle);
    }

    private static async Task<Results<Ok<PaginatedList<Game>>, ProblemHttpResult>> Handle(
        [AsParameters] GetGamesPageQuery request,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        
        return TypedResults.Ok(result);
    }

}