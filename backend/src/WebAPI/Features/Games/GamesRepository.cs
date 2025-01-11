using GameNotes.WebAPI.Database;
using GameNotes.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameNotes.WebAPI.Features.Games;

public class GamesRepository
{
    private readonly GameNotesDbContext _context;

    public GamesRepository(GameNotesDbContext context)
    {
        _context = context;
    }

    public async Task<Game?> GetByIdAsync(int id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = _context.Games.AsQueryable();
        
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        
        return await query.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<Game> CreateAsync(Game game, CancellationToken cancellationToken = default)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync(cancellationToken);
        return game;
    }
}