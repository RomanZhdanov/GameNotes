using System.Reflection;
using GameNotes.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameNotes.WebAPI.Database;

public class GameNotesDbContext(DbContextOptions<GameNotesDbContext> options) 
    : DbContext(options)
{
    public DbSet<Game> Games { get; set; }

    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Developer> Developers { get; set; }

    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<Journal> Journals { get; set; }
    
    public DbSet<JournalEntry> JournalEntries { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await NewGamesAdditionAsync();

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task NewGamesAdditionAsync()
    {
        var newGames = ChangeTracker.Entries<Game>()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified)
            .Select(e => e.Entity)
            .ToList();

        foreach (var game in newGames)
        {
            foreach (var platform in game.Platforms)
            {
                var existingPlatform = await Platforms
                    .SingleOrDefaultAsync(p => p.Id == platform.PlatformId);

                Entry(platform.Platform).State = existingPlatform == null
                    ? EntityState.Added
                    : EntityState.Unchanged;
            }

            foreach (var developer in game.Developers)
            {
                var existingDeveloper = await Developers
                    .SingleOrDefaultAsync(d => d.Id == developer.Id);

                Entry(developer).State = existingDeveloper == null
                    ? EntityState.Added
                    : EntityState.Unchanged;
            }

            foreach (var publisher in game.Publishers)
            {
                var existingPublisher = await Publishers
                    .SingleOrDefaultAsync(p => p.Id == publisher.Id);

                Entry(publisher).State = existingPublisher == null
                    ? EntityState.Added
                    : EntityState.Unchanged;
            }
        }
    }
}