using GameStoreRemake.Entities;

namespace GameStoreRemake.Repositories
{
    public interface IGamesRepositories
    {
        void Create(Game game);
        void Delete(int id);
        Game? Get(int id);
        IEnumerable<Game> GetAll();
        void Update(Game updatedGame);
    }
}