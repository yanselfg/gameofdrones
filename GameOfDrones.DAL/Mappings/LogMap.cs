using GameOfDrones.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameOfDrones.DAL.Mappings
{
    /// <summary>
    /// Represents the Log mapping configuration
    /// </summary>
    public partial class LogMap : EntityTypeConfiguration<Log>
    {
        #region Methods

        /// <summary>
        /// Configures Log entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable(nameof(Log));
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Date).IsRequired();
            builder.Property(l => l.Level).IsRequired();
            builder.Property(l => l.Message).IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}
