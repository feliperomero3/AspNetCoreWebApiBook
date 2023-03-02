namespace MyBoardGameList.Models;

public class RestModel<T>
{
    public ICollection<LinkModel> Links { get; set; } = new HashSet<LinkModel>();

    public T Data { get; set; } = default!;
}
