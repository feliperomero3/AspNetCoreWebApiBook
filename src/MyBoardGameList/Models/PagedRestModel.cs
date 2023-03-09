namespace MyBoardGameList.Models;

public class PagedRestModel<T>
{
    public ICollection<LinkModel> Links { get; set; } = new HashSet<LinkModel>();

    public T Data { get; set; } = default!;

    public int? PageIndex { get; set; }

    public int? PageSize { get; set; }

    public int? TotalCount { get; set; }
}
