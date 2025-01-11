using GameNotes.WebAPI.Entities;
using GameNotes.WebAPI.Features.Shared;

namespace GameNotes.WebAPI.Services;

public interface IGamesSourceService
{
    Task<Game?> GetGameAsync(int gameId);
    
    Task<PaginatedList<Game>> GetGamesPageAsync(int page, int pageSize, string searchInput);
}