using GameOfDrones.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameOfDrones.DAL
{
    public partial class EntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        #region Utilities

        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {

        }

        #endregion

        #region Methods
        
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //add custom configuration
            this.PostConfigure(builder);
        }
        
        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        #endregion
    }
}
