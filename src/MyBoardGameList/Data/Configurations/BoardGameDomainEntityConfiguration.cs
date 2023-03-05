using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardGameList.Entities;

namespace MyBoardGameList.Data.Configurations;

public class BoardGameDomainEntityConfiguration : IEntityTypeConfiguration<BoardGameDomain>
{
    public void Configure(EntityTypeBuilder<BoardGameDomain> builder)
    {
        builder.HasKey(t => new { t.BoardGameId, t.DomainId });

        builder.HasOne(x => x.BoardGame)
            .WithMany(y => y.BoardGameDomains)
            .HasForeignKey(f => f.BoardGameId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Domain)
            .WithMany(m => m.BoardGameDomains)
            .HasForeignKey(f => f.DomainId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
