using GameNotes.WebAPI.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameNotes.WebAPI.Features.Journals.CreateJournal;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/journals", Handle);
    }

    private static async Task<IResult> Handle(
        [FromBody] CreateJournalCommand command, 
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Results.Ok(result);
    }
}