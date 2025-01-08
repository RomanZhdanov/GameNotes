using System.Reflection;
using GameNotes.WebAPI.Extensions;
using GameNotes.WebAPI.Services;
using RawgSharp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRawgClient();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.RegisterEndpointsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IGamesSourceService, GamesSourceService>();

var app = builder.Build();

app.MapEndpoints();

app.Run();
