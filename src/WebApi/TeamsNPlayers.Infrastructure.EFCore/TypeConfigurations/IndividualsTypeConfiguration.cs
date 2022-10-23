using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamsNPlayers.Application.Individuals;

namespace TeamsNPlayers.Infrastructure.EFCore.TypeConfigurations;

public class IndividualsTypeConfiguration : IEntityTypeConfiguration<Individual>
{
    public void Configure(EntityTypeBuilder<Individual> builder)
    {
        builder
            .ToTable("individuals")
            .HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("id").IsRequired();
        builder.Property(i => i.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(i => i.LastName).HasColumnName("last_name").IsRequired();

        builder.HasData(
            new Individual { Id = new Guid("ed774e4c-5fb6-48b1-b8fa-61793cbbdc50"), FirstName = "John", LastName = "Smith" }, 
            new Individual { Id = new Guid("899340ea-841c-473f-a0d6-093d2bbe1474"), FirstName = "Jane", LastName = "Doe" },
            new Individual { Id = new Guid("4e0fc4d0-7cfb-4fc9-b5b5-51789e9787cb"), FirstName = "Edgar", LastName = "Allan Poe" }
        );
    }
}