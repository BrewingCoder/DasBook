using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DasBook.Model.Configurations;

public class UniverseConfiguration : IEntityTypeConfiguration<Universe>
{
    public void Configure(EntityTypeBuilder<Universe> builder)
    {
        builder.ToTable("Universes");
    }
}
