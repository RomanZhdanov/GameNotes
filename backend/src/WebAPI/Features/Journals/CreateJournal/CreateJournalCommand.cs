using GameNotes.WebAPI.Entities;
using GameNotes.WebAPI.Features.Games;
using GameNotes.WebAPI.Services;
using MediatR;

namespace GameNotes.WebAPI.Features.Journals.CreateJournal;

public sealed record CreateJournalCommand : IRequest<int>
{
    public int GameId { get; init; }

    public int PlatformId { get; init; }

    public bool Replay { get; init; }

    public string? Title { get; init; }
}

public class Handler : IRequestHandler<CreateJournalCommand, int>
{
    private readonly GamesRepository _gamesRepo;
    private readonly JournalsRepository _journalsRepo;
    private readonly IGamesSourceService _gamesSource;

    public Handler(GamesRepository gamesRepo, JournalsRepository journalsRepo, IGamesSourceService gamesSource)
    {
        _gamesRepo = gamesRepo;
        _journalsRepo = journalsRepo;
        _gamesSource = gamesSource;
    }

    public async Task<int> Handle(CreateJournalCommand request, CancellationToken cancellationToken)
    {
        var game = await _gamesRepo.GetByIdAsync(request.GameId, true, cancellationToken);

        if (game is null)
        {
            game = await _gamesSource.GetGameAsync(request.GameId);

            if (game is null)
            {
                throw new Exception("Game not found");
            }
            
            await _gamesRepo.CreateAsync(game, cancellationToken);
        }

        var journal = Journal.Create
        (
            request.GameId, 
            request.PlatformId, 
            request.Title, 
            request.Replay
        );
        
        journal = await _journalsRepo.CreateAsync(journal, cancellationToken);
        
        return journal.Id;
    }
}