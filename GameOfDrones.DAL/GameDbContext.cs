using GameOfDrones.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace GameOfDrones.DAL
{
    /// <summary>
    /// Represents base object context
    /// </summary>
    public class GameDbContext : DbContext, IDbContext
    {
        #region Ctor

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion

        #region Utilities
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }
        
        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter))
                {
                    continue;
                }

                sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";

                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                {
                    sql = $"{sql} output";
                }
            }

            return sql;
        }

        #endregion

        #region Methods

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return base.Set<TEntity>();
        }
        
        public virtual string GenerateCreateScript()
        {
            return Database.GenerateCreateScript();
        }
                
        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
        {
            return Query<TQuery>().FromSql(sql);
        }

        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : Entity
        {
            return Set<TEntity>().FromSql(CreateSqlWithParameters(sql, parameters), parameters);
        }

        public virtual int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            var previousTimeout = this.Database.GetCommandTimeout();
            Database.SetCommandTimeout(timeout);
            var result = 0;

            if (!doNotEnsureTransaction)
            {
                using (var transaction = Database.BeginTransaction())
                {
                    result = Database.ExecuteSqlCommand(sql, parameters);
                    transaction.Commit();
                }
            }
            else
            {
                result = this.Database.ExecuteSqlCommand(sql, parameters);
            }

            Database.SetCommandTimeout(previousTimeout);

            return result;
        }

        public virtual void Detach<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entityEntry = this.Entry(entity);

            if (entityEntry == null)
            {
                return;
            }

            entityEntry.State = EntityState.Detached;
        }

        #endregion
    }
}
