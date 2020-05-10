using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ISizeRepositoryCustom: ISizeRepositoryCustom<Size>
    {
    }

    public interface ISizeRepositoryCustom<TSize>
    {
        Task<IEnumerable<TSize>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}