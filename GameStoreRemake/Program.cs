using GameStoreRemake.Data;
using GameStoreRemake.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

await app.Services.InitializeDb();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGamesEndpoints();

app.Run();
