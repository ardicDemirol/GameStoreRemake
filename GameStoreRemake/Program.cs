using GameStoreRemake.Data;
using GameStoreRemake.Endpoints;
using GameStoreRemake.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGamesRepositories, InMemGamesRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSqlServer<ApplicationDbContext>(connectionString);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.Services.InitializeDb();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGamesEndpoints();

app.Run();
