using MediatR;

namespace GameNotes.WebAPI.Features.Journals.DeleteJournal;

public record DeleteJournalCommand(int Id) : IRequest;

public class Handler : IRequestHandler<DeleteJournalCommand>
{
    private readonly JournalsRepository _journalsRepository;

    public Handler(JournalsRepository journalsRepository)
    {
        _journalsRepository = journalsRepository;
    }

    public async Task Handle(DeleteJournalCommand request, CancellationToken cancellationToken)
    {
        await _journalsRepository.DeleteAsync(request.Id, cancellationToken);
    }
}