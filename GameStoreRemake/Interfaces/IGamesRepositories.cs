using GameStoreRemake.Entities;

namespace GameStoreRemake.Interfaces
{
    public interface IGamesRepositories
    {
        Task Create(Game game);
        Task Delete(int id);
        Task<Game?> Get(int id);
        Task<IEnumerable<Game>> GetAll();
        Task Update(Game updatedGame);
    }
}