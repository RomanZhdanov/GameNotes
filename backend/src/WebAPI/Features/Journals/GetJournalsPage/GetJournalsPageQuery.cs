using GameNotes.WebAPI.Features.Shared;
using MediatR;

namespace GameNotes.WebAPI.Features.Journals.GetJournalsPage;

public record GetJournalsPageQuery(int Page, int PageSize) : IRequest<PaginatedList<JournalDto>>;

public class Handler : IRequestHandler<GetJournalsPageQuery, PaginatedList<JournalDto>>
{
    private readonly JournalsRepository _repository;

    public Handler(JournalsRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedList<JournalDto>> Handle(GetJournalsPageQuery request, CancellationToken cancellationToken)
    {
        var journals = (await _repository.GetPageAsync(request.Page, request.PageSize, true, cancellationToken))
            .Select(j => new JournalDto(j.Id, j.Game.Name, j.Platform.Name, j.Title))
            .ToList();

        var count = await _repository.CountAsync();
        
        return new PaginatedList<JournalDto>(journals, count, request.Page, request.PageSize);
    }
}