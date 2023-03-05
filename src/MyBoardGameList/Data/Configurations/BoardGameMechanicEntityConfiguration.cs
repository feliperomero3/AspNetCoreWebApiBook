using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardGameList.Entities;

namespace MyBoardGameList.Data.Configurations;

public class BoardGameMechanicEntityConfiguration : IEntityTypeConfiguration<BoardGameMechanic>
{
    public void Configure(EntityTypeBuilder<BoardGameMechanic> builder)
    {
        builder.HasKey(t => new { t.BoardGameId, t.MechanicId });
    }
}
