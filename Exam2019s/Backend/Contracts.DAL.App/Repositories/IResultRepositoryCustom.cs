using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IResultRepositoryCustom : IResultRepositoryCustom<Result>
    {
        
    }
    public interface IResultRepositoryCustom<TResult>
    {
        Task<IEnumerable<TResult>> GetAllForViewAsync();
        Task<TResult> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TResult>> GetAllForApiAsync();
        Task<TResult> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }
}