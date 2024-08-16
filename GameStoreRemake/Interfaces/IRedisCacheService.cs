namespace GameStoreRemake.Interfaces;

public interface IRedisCacheService
{
    Task<T> GetValueAsync<T>(string key);
    Task<bool> SetValueAsync<T>(string key, T value);
    Task Clear(string key);
    void ClearAll();
}
