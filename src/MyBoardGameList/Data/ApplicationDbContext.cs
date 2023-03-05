using Microsoft.EntityFrameworkCore;
using MyBoardGameList.Data.Configurations;
using MyBoardGameList.Entities;

namespace MyBoardGameList.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<BoardGame> BoardGames => Set<BoardGame>();

    public DbSet<Domain> Domains => Set<Domain>();

    public DbSet<Mechanic> Mechanics => Set<Mechanic>();

    public DbSet<BoardGameDomain> BoardGameDomains => Set<BoardGameDomain>();

    public DbSet<BoardGameMechanic> BoardGameMechanics => Set<BoardGameMechanic>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new BoardGameEntityConfiguration())
            .ApplyConfiguration(new DomainEntityConfiguration())
            .ApplyConfiguration(new MechanicEntityConfiguration());
    }
}
