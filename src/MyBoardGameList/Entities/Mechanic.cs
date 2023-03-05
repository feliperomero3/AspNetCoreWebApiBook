﻿namespace MyBoardGameList.Entities;

public class Mechanic
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public ICollection<BoardGameMechanic> BoardGameMechanics { get; set; } = new HashSet<BoardGameMechanic>();
}
