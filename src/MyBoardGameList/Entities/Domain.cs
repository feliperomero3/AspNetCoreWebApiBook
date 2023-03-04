namespace MyBoardGameList.Entities;

public class Domain
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime LastModifiedDate { get; set; }
}
