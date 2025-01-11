using GameNotes.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameNotes.WebAPI.Database.Configurations;

public class GamesConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasMany(g => g.Developers)
            .WithMany(d => d.Games)
            .UsingEntity("GamesDevelopers");
        
        builder.HasMany(g => g.Publishers)
            .WithMany(p => p.Games)
            .UsingEntity("GamesPublishers");
    }
}