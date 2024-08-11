using GameStoreRemake.Entities;
using GameStoreRemake.Repositories;

namespace GameStoreRemake.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGameById";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games").WithParameterValidation();

        group.MapGet("/", (IGamesRepositories repository) => repository.GetAll());


        group.MapGet("/{id}", (IGamesRepositories repository, int id) =>
        {
            Game? game = repository.Get(id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        }).WithName(GetGameEndpointName);



        group.MapPost("/", (IGamesRepositories repository, Game game) =>
        {
            repository.Create(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });


        group.MapPut("/{id}", (IGamesRepositories repository, int id, Game updatedGame) =>
        {
            Game? game = repository.Get(id);

            if (game is null) return Results.NotFound();

            game.Name = updatedGame.Name;
            game.Genre = updatedGame.Genre;
            game.Price = updatedGame.Price;
            game.ReleaseDate = updatedGame.ReleaseDate;
            game.ImageUri = updatedGame.ImageUri;

            repository.Update(game);

            return Results.NoContent();

        });

        group.MapDelete("/games/{id}", (IGamesRepositories repository, int id) =>
        {
            Game? game = repository.Get(id);

            if (game is null) return Results.NotFound();

            repository.Delete(id);

            return Results.NoContent();
        });


        return group;

    }
}
