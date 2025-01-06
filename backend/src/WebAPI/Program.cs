using GameNotes.WebAPI.Features.Games;
using RawgSharp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRawgClient();
builder.Services.AddScoped<GamesSourceService>();

var app = builder.Build();

app.AddGamesEndpoints();

app.Run();
