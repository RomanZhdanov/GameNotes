namespace GameNotes.WebAPI.Entities;

public class Journal
{
    public int Id { get; private set; }

    public int GameId { get; private set; }

    public int PlatformId { get; private set; }
    
    public string? Title { get; private set; }

    public bool Replay { get; private set; }

    public Game? Game { get; private set; }

    public Platform? Platform { get; private set; }
    
    public IList<JournalEntry> Entries { get; private set; } = new List<JournalEntry>();

    public static Journal Create(
        int gameId,
        int platformId,
        string? title,
        bool replay
        )
    {
        return new Journal
        {
            GameId = gameId,
            PlatformId = platformId,
            Title = title,
            Replay = replay
        };
    }
}