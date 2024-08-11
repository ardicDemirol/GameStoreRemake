using GameStoreRemake.Entities;

namespace GameStoreRemake.Repositories;

public class InMemGamesRepository : IGamesRepositories
{
    public List<Game> games = new()
    {
        new Game()
        {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateTime(1991, 2, 1),
            ImageUri = "https://placehold.co/100"
        },
        new Game()
        {
            Id = 2,
            Name = "Super Mario Bros.",
            Genre = "Platformer",
            Price = 29.99M,
            ReleaseDate = new DateTime(1985, 9, 13),
            ImageUri = "https://placehold.co/100"
        },
        new Game()
        {
            Id = 3,
            Name = "The Legend of Zelda",
            Genre = "Action-Adventure",
            Price = 39.99M,
            ReleaseDate = new DateTime(1986, 2, 21),
            ImageUri = "https://placehold.co/100"
        }
    };

    public IEnumerable<Game> GetAll()
    {
        return games;
    }

    public Game? Get(int id)
    {
        return games.Find(x => x.Id == id);
    }

    public void Create(Game game)
    {
        game.Id = games.Max(x => x.Id) + 1;
        games.Add(game);
    }

    public void Update(Game updatedGame)
    {
        var index = games.FindIndex(x => x.Id == updatedGame.Id);
        games[index] = updatedGame;
    }

    public void Delete(int id)
    {
        var index = games.FindIndex(x => x.Id == id);
        games.RemoveAt(index);
    }

}
