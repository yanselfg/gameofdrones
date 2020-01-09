using GameOfDrones.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GameOfDrones.DAL
{
    /// <summary>
    /// Represents DB context
    /// </summary>
    public partial interface IDbContext
    {
        #region Methods
        
        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;
        
        int SaveChanges();

        string GenerateCreateScript();

        IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class;

        IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : Entity;
        
        int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        void Detach<TEntity>(TEntity entity) where TEntity : Entity;

        #endregion
    }
}
