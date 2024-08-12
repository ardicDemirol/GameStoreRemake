using GameStoreRemake.Data;
using GameStoreRemake.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStoreRemake.Repositories;

public class EntityFrameworkGamesRepository : IGamesRepositories
{
    private readonly ApplicationDbContext _dbContext;

    public EntityFrameworkGamesRepository(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<IEnumerable<Game>> GetAll()
    {
        return await _dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task<Game?> Get(int id)
    {
        return await _dbContext.Games.FindAsync(id);
    }

    public async Task Create(Game game)
    {
        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Game updatedGame)
    {
        _dbContext.Update(updatedGame);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        await _dbContext.Games.Where(game => game.Id == id)
                            .ExecuteDeleteAsync();
    }

}