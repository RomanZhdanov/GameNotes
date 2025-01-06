namespace GameNotes.WebAPI.Entities;

public class Game
{
    public int Id { get; set; }

    public string Slug { get; set; }

    public string Name { get; set; }

    public string NameOriginal { get; set; }

    public string Description { get; set; }

    public double? Metacritic { get; set; }

    public DateTime? Released { get; set; }

    public bool TBA { get; set; }

    public string BackgroundImage { get; set; }

    public string BackgroundImageAdditional { get; set; }

    public string Website { get; set; }

    public IList<GamePlatform> Platforms { get; set; }

    public IList<Developer> Developers { get; set; }

    public IList<Publisher> Publishers { get; set; }
}