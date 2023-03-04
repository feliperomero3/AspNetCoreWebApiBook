using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardGameList.Entities;

namespace MyBoardGameList.Data.Configurations;

public class DomainEntityConfiguration : IEntityTypeConfiguration<Domain>
{
    public void Configure(EntityTypeBuilder<Domain> builder)
    {
        builder.ToTable("Domains").HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(64).IsRequired();
    }
}
