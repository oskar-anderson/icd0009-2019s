using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        //get thing done!
        int SaveChanges();
        Task<int> SaveChangesAsync();

        TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod);

    }
}