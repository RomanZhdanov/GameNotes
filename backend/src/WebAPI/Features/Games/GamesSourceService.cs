using GameNotes.WebAPI.Entities;
using GameNotes.WebAPI.Features.Shared;
using RawgSharp;

namespace GameNotes.WebAPI.Features.Games;

public class GamesSourceService(IRawgApi rawgApi)
{
    private readonly IRawgApi _rawgApi = rawgApi;

    public async Task<PaginatedList<Game>> GetGamesPageAsync(int page, int pageSize, string searchInput)
    {
        var response = await _rawgApi.GetListOfGamesAsync(page, pageSize, searchInput);
        
        var items = response.Results.Select(r => new Game
        {
            Id = r.Id,
            Slug = r.Slug,
            Name = r.Name,
            Released = r.Released,
            TBA = r.TBA,
            BackgroundImage = r.BackgroundImage,
            Metacritic = r.Metacritic,
            Platforms = r.Platforms.Select(p => new GamePlatform
                {
                    GameId = r.Id,
                    PlatformId = p.Platform.Id,
                    Platform = new Platform
                    {
                        Id = p.Platform.Id,
                        Slug = p.Platform.Slug,
                        Name = p.Platform.Name
                    },
                    ReleasedAt = string.IsNullOrEmpty(p.ReleasedAt) ? null : DateTime.Parse(p.ReleasedAt)
                })
                .OrderBy(p => p.ReleasedAt)
                .ToList()
        });
        return new PaginatedList<Game>(
            items,
            response.Count,
            page,
            pageSize);
    }
}