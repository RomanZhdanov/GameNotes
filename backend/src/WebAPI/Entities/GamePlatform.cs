namespace GameNotes.WebAPI.Entities;

public class GamePlatform
{
    public int GameId { get; set; }

    public int PlatformId { get; set; }
    
    public DateTime? ReleasedAt { get; set; }

    public Platform Platform { get; set; }

    public Game Game { get; set; }
}