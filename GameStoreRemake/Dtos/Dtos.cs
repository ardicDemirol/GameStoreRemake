using System.ComponentModel.DataAnnotations;

namespace GameStoreRemake.Dtos;
public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUri
);

public record CreateGameDto(
    [Required][StringLength(75)] string Name,
    [Required][StringLength(30)] string Genre,
    [Range(1, 100)] decimal Price,
    DateTime ReleaseDate,
    [Url][StringLength(200)] string ImageUri
);

public record UpdateGameDto(
    [Required][StringLength(75)] string Name,
    [Required][StringLength(30)] string Genre,
    [Range(1, 100)] decimal Price,
    DateTime ReleaseDate,
    [Url][StringLength(200)] string ImageUri
);

