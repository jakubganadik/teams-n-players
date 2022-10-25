using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsNPlayers.Application.Individuals;
using TeamsNPlayers.Application.Players;
using TeamsNPlayers.Application.Teams;

namespace TeamsNPlayers.Infrastructure.EFCore.TypeConfigurations
{
    internal class PlayersTypeConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder
                .ToTable("players")
                .HasKey(i => i.Id);

            builder.Property(i => i.Id).HasColumnName("id").IsRequired();
            builder.Property(i => i.TeamId).HasColumnName("team_id").IsRequired();
            builder.Property(i => i.IndividualId).HasColumnName("individual_id").IsRequired();
            //builder.Property(i => i.TeamName).HasColumnName("team_name").IsRequired();
            //builder.Property(i => i.PlayerName).HasColumnName("player_name").IsRequired();
            builder.Property(i => i.ShirtNumber).HasColumnName("shirt_number").IsRequired();
            builder.Property(i => i.Position).HasColumnName("position").IsRequired();
            builder.HasOne<Team>(x => x.Team).WithMany().HasForeignKey(x => x.TeamId).IsRequired();
            builder.HasOne<Individual>(x => x.Individual).WithMany().HasForeignKey(x => x.IndividualId).IsRequired();
            
            builder.HasData(
                new Player { Id = new Guid("ed775e4c-5fb6-48b1-b8fa-61793cbbdc50"), TeamId = new Guid("79d1705c-4864-4a29-a864-2ec582bb6342"), IndividualId = new Guid("ed774e4c-5fb6-48b1-b8fa-61793cbbdc50"),  ShirtNumber = 77, Position = "GLK"}
            );
            

        }
    }
}
