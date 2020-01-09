using GameOfDrones.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameOfDrones.DAL.Mappings
{
    /// <summary>
    /// Represents the Game mapping configuration
    /// </summary>
    public partial class GameMap : EntityTypeConfiguration<Game>
    {
        #region Methods

        /// <summary>
        /// Configures Game entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable(nameof(Game));
            builder.HasKey(r => r.Id);

            builder.HasOne(g => g.Winner)
                .WithMany(p => p.GamesWon)
                .HasForeignKey(g => g.WinnerId)
                .IsRequired(false);

            builder.HasMany(g => g.Rounds)
                .WithOne(r => r.Game)
                .HasForeignKey(g => g.GameId)
                .IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}
