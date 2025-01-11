namespace GameNotes.WebAPI.Entities;

public class Journal
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public int PlatformId { get; set; }
    
    public string? Title { get; set; }

    public bool Replay { get; set; }

    public Game? Game { get; set; }

    public Platform? Platform { get; set; }
    
    public IList<JournalEntry> Entries { get; set; } = new List<JournalEntry>();
}