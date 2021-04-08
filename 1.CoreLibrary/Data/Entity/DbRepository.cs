﻿using CoreLibrary.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;

namespace CoreLibrary.Data.Entity
{
    public class DbRepository<TEntity> : IDbRepository<TEntity>
        where TEntity : class
    {
        Type entityType;
        readonly IDbSet<TEntity> dbSet;
        readonly IQueryable<TEntity> dbSetIQueryable;
        readonly bool permanentDelete;
        readonly bool getDeleted;
        readonly Func<IPrincipal> getCurrentUser;

        public DbRepository(IDbSet<TEntity> dbSet, Func<IPrincipal> getCurrentUser, bool getDeleted = false, bool permanentDelete = false)
        {
            if (dbSet == null) throw new ArgumentNullException("dbSet");

            this.entityType = typeof(TEntity);
            this.dbSet = dbSet;
            this.dbSetIQueryable = dbSet as IQueryable<TEntity>;
            this.permanentDelete = permanentDelete;
            this.getDeleted = getDeleted;
            this.getCurrentUser = getCurrentUser;
        }

        public TEntity Add(TEntity entity)
        {
            return dbSet.Add(entity);
        }

        public TEntity Attach(TEntity entity)
        {
            return dbSet.Attach(entity);
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return dbSet.Create<TDerivedEntity>();
        }

        public TEntity Create()
        {
            return dbSet.Create();
        }

        public TEntity Find(params object[] keyValues)
        {
            return dbSet.Find(keyValues);
        }

        public System.Collections.ObjectModel.ObservableCollection<TEntity> Local
        {
            get { return dbSet.Local; }
        }

        public TEntity Remove(TEntity entity)
        {
            return dbSet.Remove(entity);
        }

        #region IEnumerable Members

        public IEnumerator<TEntity> GetEnumerator()
        {
            return dbSetIQueryable.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        public Type ElementType
        {
            get { return dbSetIQueryable.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return dbSetIQueryable.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return dbSetIQueryable.Provider; }
        }

        #endregion
    }
}
