namespace MyBoardGameList.Entities;

public class Mechanic
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public DateTime LastModifiedDate { get; private set; }

    public ICollection<BoardGameMechanic> BoardGameMechanics { get; private set; } = new HashSet<BoardGameMechanic>();

    public Mechanic(string name)
    {
        Name = name;
        CreatedDate = DateTime.Now;
        LastModifiedDate = DateTime.Now;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Mechanic other)
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
