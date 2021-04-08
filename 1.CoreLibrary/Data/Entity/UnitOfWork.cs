using CoreLibrary.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using WebMatrix.WebData;

namespace CoreLibrary.Data.Entity
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly Dictionary<Type, IDbRepository> _cachedRepositories = new Dictionary<Type, IDbRepository>();
        readonly DbContext _dbContext;
        readonly Func<IPrincipal> _getCurrentUser;

        public UnitOfWork(DbContext dbContext, Func<IPrincipal> getCurrentUser)
        {
            this._dbContext = dbContext;
            this._getCurrentUser = getCurrentUser;
        }

        public int SaveChanges()
        {
            var changeSet = _dbContext.ChangeTracker.Entries();

            if (changeSet != null)
            {
                DateTime currentDateTime = DateTime.UtcNow;
                Type type;
                string currentUsername = !string.IsNullOrEmpty(WebSecurity.CurrentUserName) ? WebSecurity.CurrentUserName : "System";

                foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
                {
                    type = entry.Entity.GetType();
                    
                    if (entry.State == EntityState.Added)
                    {
                        type.GetProperty(Constant.FieldNames.CreatedAt)?.SetValue(entry.Entity, currentDateTime, null);
                        type.GetProperty(Constant.FieldNames.CreatedBy)?.SetValue(entry.Entity, currentUsername, null);
                    }
                    if(entry.State == EntityState.Modified)
                    {
                        type.GetProperty(Constant.FieldNames.UpdatedAt)?.SetValue(entry.Entity, currentDateTime, null);
                        type.GetProperty(Constant.FieldNames.UpdatedBy)?.SetValue(entry.Entity, currentUsername, null);
                    }
                }
            }

            return _dbContext.SaveChanges();
        }

        public IDbRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            if (_cachedRepositories.ContainsKey(type))
            {
                return _cachedRepositories[type] as IDbRepository<TEntity>;
            }
            else
            {
                var repo = new DbRepository<TEntity>(_dbContext.Set<TEntity>(), _getCurrentUser);
                _cachedRepositories[type] = repo;
                return repo;
            }
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
