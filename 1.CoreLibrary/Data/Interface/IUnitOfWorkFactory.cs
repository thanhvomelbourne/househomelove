using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Data.Interface
{
    public interface IUnitOfWorkFactory<out TUnitOfWork> where TUnitOfWork : IUnitOfWork
    {
        void CreateDatabase(bool dropIfExist, bool seedData);

        void DeleteDatabase();

        TUnitOfWork Create();
    }
}
