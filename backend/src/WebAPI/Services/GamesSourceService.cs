using GameNotes.WebAPI.Entities;
using GameNotes.WebAPI.Features.Shared;
using RawgSharp;

namespace GameNotes.WebAPI.Services;

public class GamesSourceService(IRawgApi rawgApi) : IGamesSourceService
{
    private readonly IRawgApi _rawgApi = rawgApi;

    public async Task<Game?> GetGameAsync(int gameId)
    {
        var game = await _rawgApi.GetGameDetails(gameId);

        var platformList = game.Platforms.Select(p => new GamePlatform
            {
                GameId = gameId,
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
            .ToList();
        
        return new Game
        {
            Id = game.Id,
            Slug = game.Slug,
            Name = game.Name,
            NameOriginal = game.NameOriginal,
            Description = game.Description,
            Metacritic = game.Metacritic,
            Released = game.Released,
            TBA = game.TBA,
            BackgroundImage = game.BackgroundImage,
            BackgroundImageAdditional = game.BackgroundImageAdditional,
            Website = game.Website,
            Platforms = platformList,
            Developers = game.Developers.Select(d => new Developer
            {
                Id = d.Id,
                Name = d.Name,
                Slug = d.Slug
            }).ToList(),
            Publishers = game.Publishers.Select(p => new Publisher
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug
            }).ToList()
        };
    }
    
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