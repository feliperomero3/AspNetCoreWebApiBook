namespace MyBoardGameList.Entities;

public class BoardGameDomain
{
    public int BoardGameId { get; set; }

    public int DomainId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public BoardGame? BoardGame { get; set; }

    public Domain? Domain { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not BoardGameDomain other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(BoardGame!.Name, other.BoardGame!.Name, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(Domain!.Name, other.Domain!.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + BoardGame!.Name + Domain!.Name).GetHashCode();
    }
}
