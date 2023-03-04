using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardGameList.Entities;

namespace MyBoardGameList.Data.Configurations;

public class BoardGameEntityConfiguration : IEntityTypeConfiguration<BoardGame>
{
    public void Configure(EntityTypeBuilder<BoardGame> builder)
    {
        builder.ToTable("BoardGames").HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Year).IsRequired();
        builder.Property(p => p.MinPlayers).IsRequired();
        builder.Property(p => p.MaxPlayers).IsRequired();
        builder.Property(p => p.PlayTime).IsRequired();
        builder.Property(p => p.MinAge).IsRequired();
        builder.Property(p => p.UsersRated).IsRequired();
        builder.Property(p => p.RatingAverage).IsRequired().HasPrecision(4, 2);
        builder.Property(p => p.BGGRank).IsRequired();
        builder.Property(p => p.ComplexityAverage).IsRequired().HasPrecision(4, 2);
        builder.Property(p => p.OwnedUsers).IsRequired();
        builder.Property(p => p.CreatedDate).IsRequired();
        builder.Property(p => p.LastModifiedDate).IsRequired();
    }
}
