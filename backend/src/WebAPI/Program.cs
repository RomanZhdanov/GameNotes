using GameNotes.WebAPI.Games;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.AddGamesEndpoints();

app.Run();
