namespace MyBoardGameList.Entities;

public class BoardGameMechanic
{
    public int BoardGameId { get; set; }

    public int MechanicId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public BoardGame? BoardGame { get; set; }

    public Mechanic? Mechanic { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not BoardGameMechanic other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(BoardGame!.Name, other.BoardGame!.Name, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(Mechanic!.Name, other.Mechanic!.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + BoardGame!.Name + Mechanic!.Name).GetHashCode();
    }
}
