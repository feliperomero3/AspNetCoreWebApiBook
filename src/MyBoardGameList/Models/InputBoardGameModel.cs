using System.ComponentModel.DataAnnotations;

namespace MyBoardGameList.Models;

public class InputBoardGameModel
{
    [Required]
    [StringLength(64)]
    public string? Name { get; set; }

    [Required]
    public int? Year { get; set; }

    [Required]
    public int? MinPlayers { get; set; }

    [Required]
    public int? MaxPlayers { get; set; }
}
