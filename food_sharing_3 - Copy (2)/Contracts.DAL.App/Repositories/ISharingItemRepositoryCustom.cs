using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ISharingItemRepositoryCustom: ISharingItemRepositoryCustom<SharingItem>
    {
    }

    public interface ISharingItemRepositoryCustom<TSharingItem>
    {
        Task<IEnumerable<TSharingItem>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}