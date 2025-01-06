namespace GameNotes.WebAPI.Entities;

public class Developer
{
    public int Id { get; set; }
    
    public string Slug { get; set; }
    
    public string Name { get; set; }

    public IList<Game> Games { get; set; }
}