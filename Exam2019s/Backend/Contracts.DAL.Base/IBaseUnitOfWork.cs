using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        Task<int> SaveChangesAsync();

        TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
            where TRepository : class;
    }
}