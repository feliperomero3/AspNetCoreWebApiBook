using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            .ApplyConfiguration(new MechanicEntityConfiguration())
            .ApplyConfiguration(new BoardGameMechanicEntityConfiguration())
            .ApplyConfiguration(new BoardGameDomainEntityConfiguration());
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntities = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var entity in modifiedEntities)
        {
            if (entity is BoardGame game)
            {
                game.UpdateLastModifiedDate(DateTime.Now);
            }
            else if (entity is Mechanic mechanic)
            {
                mechanic.UpdateLastModifiedDate(DateTime.Now);
            }
            else if (entity is Domain domain)
            {
                domain.UpdateLastModifiedDate(DateTime.Now);
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
