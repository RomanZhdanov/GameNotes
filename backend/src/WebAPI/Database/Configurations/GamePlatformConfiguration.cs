using GameNotes.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameNotes.WebAPI.Database.Configurations;

public class GamePlatformConfiguration : IEntityTypeConfiguration<GamePlatform>
{
    public void Configure(EntityTypeBuilder<GamePlatform> builder)
    {
        builder.ToTable("GamesPlatforms")
            .HasKey(gp => new { gp.GameId, gp.PlatformId });
        
        builder.HasOne(gp => gp.Game)
            .WithMany(g => g.Platforms)
            .HasForeignKey(gp => gp.GameId);
        
        builder.HasOne(gp => gp.Platform)
            .WithMany(p => p.Games)
            .HasForeignKey(gp => gp.PlatformId);

    }
}