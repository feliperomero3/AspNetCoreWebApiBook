namespace MyBoardGameList.Models;

public class DomainModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime LastModifiedDate { get; set; }
}
