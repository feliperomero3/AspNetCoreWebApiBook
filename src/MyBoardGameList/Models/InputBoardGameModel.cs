using System.ComponentModel.DataAnnotations;

namespace MyBoardGameList.Models;

public class InputBoardGameModel
{
    [Required]
    [StringLength(64)]
    public string? Name { get; set; }

    [Required]
    [Range(1901, 2099)]
    public int? Year { get; set; }

    [Required(ErrorMessage = "This value is required.")]
    [Range(1, 100, ErrorMessage = "The value must be between 1 and 100.")]
    public int? MinPlayers { get; set; }

    [Required(ErrorMessage = "This value is required.")]
    [Range(1, 100, ErrorMessage = "The value must be between 1 and 100.")]
    public int? MaxPlayers { get; set; }
}
