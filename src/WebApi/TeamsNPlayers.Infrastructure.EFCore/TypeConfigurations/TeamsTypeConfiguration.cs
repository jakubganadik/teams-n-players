using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamsNPlayers.Application.Individuals;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.Infrastructure.EFCore.TypeConfigurations;

public class TeamsTypeConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder
            .ToTable("teams")
            .HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("id").IsRequired();
        builder.Property(i => i.Name).HasColumnName("name").IsRequired();

        builder.HasData(
            new Team { Id = new Guid("79d1705c-4864-4a29-a864-2ec582bb6342"), Name = "FC Foo" }, 
            new Team { Id = new Guid("5737a2e9-02d8-4a9e-87d1-6808954d7de3"), Name = "AC Bar" }
        );
    }
}