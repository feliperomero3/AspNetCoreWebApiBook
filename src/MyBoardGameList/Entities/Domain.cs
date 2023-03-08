namespace MyBoardGameList.Entities;

public class Domain
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public DateTime LastModifiedDate { get; private set; }

    public ICollection<BoardGameDomain> BoardGameDomains { get; private set; } = new HashSet<BoardGameDomain>();

    public Domain(string name)
    {
        Name = name;
        CreatedDate = DateTime.Now;
        LastModifiedDate = DateTime.Now;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Domain other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Name).GetHashCode();
    }
}
