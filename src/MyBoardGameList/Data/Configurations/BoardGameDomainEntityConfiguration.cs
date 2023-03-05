using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardGameList.Entities;

namespace MyBoardGameList.Data.Configurations;

public class BoardGameDomainEntityConfiguration : IEntityTypeConfiguration<BoardGameDomain>
{
    public void Configure(EntityTypeBuilder<BoardGameDomain> builder)
    {
        builder.HasKey(t => new { t.BoardGameId, t.DomainId });
    }
}
