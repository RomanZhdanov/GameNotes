using GameNotes.WebAPI.Database;
using GameNotes.WebAPI.Entities;

namespace GameNotes.WebAPI.Features.Journals;

public class JournalsRepository
{
    private readonly GameNotesDbContext _context;

    public JournalsRepository(GameNotesDbContext context)
    {
        _context = context;
    }

    public async Task<Journal> CreateAsync(Journal journal, CancellationToken cancellationToken = default)
    {
        _context.Journals.Add(journal);
        await _context.SaveChangesAsync(cancellationToken);
        return journal;
    }
}