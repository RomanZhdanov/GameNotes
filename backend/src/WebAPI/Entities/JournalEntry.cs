namespace GameNotes.WebAPI.Entities;

public class JournalEntry
{
    public int Id { get; set; }

    public int JournalId { get; set; }

    public JournalEntryTypes Type { get; set; }

    public float Rating { get; set; }

    public string? Notes { get; set; }

    public DateTime Date { get; set; }
    
    public Journal? Journal { get; set; }
}