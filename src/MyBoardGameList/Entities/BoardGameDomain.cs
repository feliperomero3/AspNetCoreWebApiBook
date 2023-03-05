namespace MyBoardGameList.Entities;

public class BoardGameDomain
{
    public int BoardGameId { get; set; }

    public int DomainId { get; set; }

    public DateTime CreatedDate { get; set; }

    public BoardGame? BoardGame { get; set; }

    public Domain? Domain { get; set; }
}
