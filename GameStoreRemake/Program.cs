using GameStoreRemake.Data;
using GameStoreRemake.Endpoints;
using GameStoreRemake.Interfaces;
using GameStoreRemake.Services;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



////////////////////////////////////////////////

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configurationOptions = ConfigurationOptions.Parse("localhost:6379");
    return ConnectionMultiplexer.Connect(configurationOptions);
});

builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

var app = builder.Build();

////////////////////////////////////////////////////////////////////

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGamesEndpoints();

app.Run();
