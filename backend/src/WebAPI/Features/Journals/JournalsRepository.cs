using GameNotes.WebAPI.Database;
using GameNotes.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameNotes.WebAPI.Features.Journals;

public class JournalsRepository
{
    private readonly GameNotesDbContext _context;

    public JournalsRepository(GameNotesDbContext context)
    {
        _context = context;
    }

    public async Task<int> CountAsync()
    {
        return await _context.Journals.CountAsync();
    }

    public async Task<IEnumerable<Journal>> GetPageAsync(int page, int pageSize, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = _context.Journals.AsQueryable();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Journal> CreateAsync(Journal journal, CancellationToken cancellationToken = default)
    {
        _context.Journals.Add(journal);
        await _context.SaveChangesAsync(cancellationToken);
        return journal;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var journal = await _context.Journals.FindAsync(id);

        if (journal is null)
        {
            throw new Exception("Journal not found");
        }
        
        _context.Journals.Remove(journal);
        await _context.SaveChangesAsync(cancellationToken);
    }
}