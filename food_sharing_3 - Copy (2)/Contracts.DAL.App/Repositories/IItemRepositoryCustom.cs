using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IItemRepositoryCustom: IItemRepositoryCustom<Item>
    {
    }

    public interface IItemRepositoryCustom<TItem>
    {
        Task<IEnumerable<TItem>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}