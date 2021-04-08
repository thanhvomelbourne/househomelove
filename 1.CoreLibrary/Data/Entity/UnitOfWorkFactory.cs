using CoreLibrary.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Data.Entity
{
    public class UnitOfWorkFactory<TUnitOfWork, TDbContext> : IUnitOfWorkFactory<TUnitOfWork>
       where TUnitOfWork : UnitOfWork, IUnitOfWork
       where TDbContext : DbContext
    {
        private readonly string _connectionStringName;
        readonly Func<IPrincipal> _getCurrentUser;

        public UnitOfWorkFactory(string connectionStringName, Func<IPrincipal> getCurrentUser)
        {
            this._connectionStringName = connectionStringName;
            this._getCurrentUser = getCurrentUser;
        }

        public void CreateDatabase(bool dropIfExist, bool seedData)
        {
            throw new NotImplementedException();
        }

        public void DeleteDatabase()
        {
            throw new NotImplementedException();
        }
        
        public virtual TUnitOfWork Create()
        {
            var ctx = Activator.CreateInstance(typeof(TDbContext), _connectionStringName) as TDbContext;
            return Activator.CreateInstance(typeof(TUnitOfWork), ctx, _getCurrentUser) as TUnitOfWork;
        }
    }
}
