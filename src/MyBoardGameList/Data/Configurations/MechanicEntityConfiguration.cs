using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardGameList.Entities;

namespace MyBoardGameList.Data.Configurations;

public class MechanicEntityConfiguration : IEntityTypeConfiguration<Mechanic>
{
    public void Configure(EntityTypeBuilder<Mechanic> builder)
    {
        builder.ToTable("Mechanics").HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(64).IsRequired();
    }
}
