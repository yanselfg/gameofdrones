using GameOfDrones.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameOfDrones.DAL.Mappings
{
    /// <summary>
    /// Represents the Round mapping configuration
    /// </summary>
    public partial class RoundMap : EntityTypeConfiguration<Round>
    {
        #region Methods

        /// <summary>
        /// Configures Round entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Round> builder)
        {
            builder.ToTable(nameof(Round));
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Game)
                .WithMany(g => g.Rounds)
                .HasForeignKey(r => r.GameId)
                .IsRequired();

            builder.HasOne(r => r.Winner)
                .WithMany()
                .HasForeignKey(r => r.WinnerId)
                .IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}
