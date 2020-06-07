using System;
using System.Threading.Tasks;

namespace ee.itcollege.kaande.pitsariina.Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        Task<int> SaveChangesAsync();

        TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
            where TRepository : class;
    }
}