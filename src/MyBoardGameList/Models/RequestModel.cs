using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyBoardGameList.Validators;

namespace MyBoardGameList.Models;

public class RequestModel
{
    [DefaultValue(0)]
    [Range(0, int.MaxValue)]
    public int PageIndex { get; set; } = 0;

    [DefaultValue(10)]
    [Range(1, 100)]
    public int PageSize { get; set; } = 10;

    [DefaultValue("ASC")]
    [SortOrderValidator]
    public string SortOrder { get; set; } = "ASC";

    [DefaultValue(null)]
    [StringLength(64)]
    public string? FilterQuery { get; set; }
}
