using GameStoreRemake.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace GameStoreRemake.Services;

public class RedisCacheService : IRedisCacheService
{
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly IDatabase _database;
    //private TimeSpan ExpireTime => TimeSpan.FromMinutes(1);
    private TimeSpan ExpireTime => TimeSpan.FromSeconds(15);

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _redisConnection = connectionMultiplexer;
        _database = connectionMultiplexer.GetDatabase();
    }
    public async Task Clear(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    public void ClearAll()
    {
        var redisEndpoints = _redisConnection.GetEndPoints(true);
        foreach (var endpoint in redisEndpoints)
        {
            var redisServer = _redisConnection.GetServer(endpoint);
            redisServer.FlushAllDatabases();
        }
    }

    public async Task<T> GetValueAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);

        return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value);
    }

    public async Task<bool> SetValueAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);

        if (json is null) return default;

        if (GetValueAsync<string>(key).Result == null)
            return await _database.StringSetAsync(key, json, ExpireTime);
        else
            return false;
    }


}
