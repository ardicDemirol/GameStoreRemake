using GameStoreRemake.Dtos;
using GameStoreRemake.Entities;
using GameStoreRemake.Interfaces;

namespace GameStoreRemake.Services;

public static class CacheExtensions
{
    public static async Task<List<GameDto>> GetGamesAsync(this IGamesRepositories repository, IRedisCacheService cacheService, string cacheKey)
    {
        // get data from redis
        var cachedGames = await cacheService.GetValueAsync<List<GameDto>>(cacheKey);

        if (cachedGames != null)
        {
            // if the data is exist on redis, return it
            return cachedGames;
        }

        //  if the data is not exist on redis, get the data from database
        var games = await repository.GetAll();
        var gameDtos = games.Select(game => game.AsDto()).ToList();

        // Cache the data to Redis
        await cacheService.SetValueAsync(cacheKey, gameDtos);

        return gameDtos;
    }


    public static async Task<GameDto> GetGameByIdAsync(this IGamesRepositories repository, IRedisCacheService cacheService, int id, string cacheKey)
    {
        var cachedGame = await cacheService.GetValueAsync<GameDto>(cacheKey);

        if (cachedGame != null)
        {
            // if the data is exist on redis, return it
            return cachedGame;
        }

        //  if the data is not exist on redis, get the data from database
        var game = await repository.Get(id);

        if (game is null) return null;

        // Cache the data to Redis
        await cacheService.SetValueAsync(cacheKey, game);

        return game.AsDto();
    }
}
