using GameNotes.WebAPI.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameNotes.WebAPI.Features.Journals.DeleteJournal;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapDelete("/journals/{id}", Handle);
    }

    private static async Task<IResult> Handle(
        [FromRoute] int id,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteJournalCommand(id), cancellationToken);
        
        return Results.NoContent();
    }
}