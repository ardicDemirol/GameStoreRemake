using GameStoreRemake.Dtos;
using GameStoreRemake.Entities;
using GameStoreRemake.Interfaces;
using GameStoreRemake.Services;
using System.Text;

namespace GameStoreRemake.Endpoints;

public static class GamesEndpoints
{
    const string GetGameByIdEndpointName = "GetGameById";
    const string GetGameByIdCachingName = "ALL_GAMES";
    const string GetGameCachingName = "GAME";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games").WithParameterValidation();

        group.MapGet("/", async (IGamesRepositories repository, IRedisCacheService cacheService) =>
        {
            return await CacheExtensions.GetGamesAsync(repository, cacheService, GetGameByIdCachingName);
        });

        group.MapGet("/{id}", async (IGamesRepositories repository, IRedisCacheService cacheService, int id) =>
            {
                //return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();

                var builder = new StringBuilder(GetGameCachingName);
                builder.Append(id);
                var fullCacheKey = builder.ToString();

                var gameDto = await CacheExtensions.GetGameByIdAsync(repository, cacheService, id, fullCacheKey);

                return gameDto != null ? Results.Ok(gameDto) : Results.NotFound();

            }).WithName(GetGameByIdEndpointName);



        group.MapPost("/", async (IGamesRepositories repository, CreateGameDto gameDto, IRedisCacheService cache) =>
                    {
                        await cache.SetValueAsync("newGame", gameDto.Name);

                        Game game = new()
                        {
                            Name = gameDto.Name,
                            Genre = gameDto.Genre,
                            Price = gameDto.Price,
                            ReleaseDate = gameDto.ReleaseDate,
                            ImageUri = gameDto.ImageUri
                        };

                        await repository.Create(game);

                        return Results.CreatedAtRoute(GetGameByIdEndpointName, new
                        {
                            id = game.Id
                        }, game);
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
