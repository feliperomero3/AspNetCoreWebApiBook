using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardGameList.Entities;

namespace MyBoardGameList.Data.Configurations;

public class BoardGameMechanicEntityConfiguration : IEntityTypeConfiguration<BoardGameMechanic>
{
    public void Configure(EntityTypeBuilder<BoardGameMechanic> builder)
    {
        builder.HasKey(t => new { t.BoardGameId, t.MechanicId });

        builder.HasOne(x => x.BoardGame)
            .WithMany(y => y.BoardGameMechanics)
            .HasForeignKey(f => f.BoardGameId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Mechanic)
            .WithMany(m => m.BoardGameMechanics)
            .HasForeignKey(f => f.MechanicId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
