using GameNotes.WebAPI.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GameNotes.WebAPI.Features.Journals.GetJournalsPage;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/journals", Handle);
    }

    private static async Task<Results<Ok<PaginatedList<JournalDto>>, ProblemHttpResult>> Handle(
        [AsParameters] GetJournalsPageQuery query,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return TypedResults.Ok(result);
    }
}