using GameNotes.WebAPI.Entities;
using GameNotes.WebAPI.Features.Shared;
using GameNotes.WebAPI.Services;
using MediatR;

namespace GameNotes.WebAPI.Features.Games.GetGamesPage;

public sealed record GetGamesPageQuery(
    int Page, 
    int PageSize, 
    string Search) 
    : IRequest<PaginatedList<Game>>;
    
internal sealed class Handler : IRequestHandler<GetGamesPageQuery, PaginatedList<Game>>
{
    private readonly IGamesSourceService _gamesSourceService;

    public Handler(IGamesSourceService gamesSourceService)
    {
        _gamesSourceService = gamesSourceService;
    }

    public async Task<PaginatedList<Game>> Handle(GetGamesPageQuery request, CancellationToken cancellationToken)
    {
        return await _gamesSourceService
            .GetGamesPageAsync(request.Page, request.PageSize, request.Search);
    }
}