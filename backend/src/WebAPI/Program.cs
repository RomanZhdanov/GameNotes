using System.Reflection;
using GameNotes.WebAPI.Database;
using GameNotes.WebAPI.Extensions;
using GameNotes.WebAPI.Features.Games;
using GameNotes.WebAPI.Features.Journals;
using GameNotes.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using RawgSharp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameNotesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRawgClient();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.RegisterEndpointsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IGamesSourceService, GamesSourceService>();
builder.Services.AddScoped<GamesRepository>();
builder.Services.AddScoped<JournalsRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameNotesDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.MapEndpoints();

app.Run();
