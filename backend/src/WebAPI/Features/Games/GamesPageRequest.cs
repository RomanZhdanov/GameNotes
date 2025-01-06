namespace GameNotes.WebAPI.Features.Games;

public record struct GamesPageRequest(
    int Page, 
    int PageSize, 
    string Search);