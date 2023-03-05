namespace MyBoardGameList.Entities;

public class BoardGameMechanic
{
    public int BoardGameId { get; set; }

    public int MechanicId { get; set; }

    public DateTime CreatedDate { get; set; }

    public BoardGame? BoardGame { get; set; }

    public Mechanic? Mechanic { get; set; }
}
