using Microsoft.EntityFrameworkCore;
using MyBoardGameList.Data.Configurations;

namespace MyBoardGameList.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new BoardGameEntityConfiguration());
    }
}
