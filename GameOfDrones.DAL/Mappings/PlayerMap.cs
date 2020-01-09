using GameOfDrones.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameOfDrones.DAL.Mappings
{
    /// <summary>
    /// Represents the Player mapping configuration
    /// </summary>
    public partial class PlayerMap : EntityTypeConfiguration<Player>
    {
        #region Methods

        /// <summary>
        /// Configures Player entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable(nameof(Player));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            //builder.Property(p => p.Gender).IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}
