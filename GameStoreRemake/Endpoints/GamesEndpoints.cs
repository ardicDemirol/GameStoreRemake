using GameStoreRemake.Dtos;
using GameStoreRemake.Entities;
using GameStoreRemake.Repositories;

namespace GameStoreRemake.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGameById";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games").WithParameterValidation();

        group.MapGet("/", async (IGamesRepositories repository) =>
            (await repository.GetAll()).Select(game => game.AsDto()));


        group.MapGet("/{id}", async (IGamesRepositories repository, int id) =>
        {
            Game? game = await repository.Get(id);
            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
        }).WithName(GetGameEndpointName);



        group.MapPost("/", async (IGamesRepositories repository, CreateGameDto gameDto) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            await repository.Create(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


        group.MapPut("/{id}", async (IGamesRepositories repository, int id, UpdateGameDto updatedGameDto) =>
        {
            Game? existingGame = await repository.Get(id);

            if (existingGame is null) return Results.NotFound();

            existingGame.Name = updatedGameDto.Name;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.Price = updatedGameDto.Price;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate;
            existingGame.ImageUri = updatedGameDto.ImageUri;

            await repository.Update(existingGame);

            return Results.NoContent();

        });

        group.MapDelete("/{id}", async (IGamesRepositories repository, int id) =>
        {
            Game? game = await repository.Get(id);

            if (game is null) return Results.NotFound();

            await repository.Delete(id);

            return Results.NoContent();
        });


        return group;

    }
}
