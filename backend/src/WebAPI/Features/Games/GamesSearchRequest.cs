namespace GameNotes.WebAPI.Features.Games;

public sealed record GamesSearchRequest(
    int Page, 
    int PageSize, 
    string Input);