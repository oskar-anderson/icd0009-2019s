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
        Task<IEnumerable<TSharingItem>> GetAllForViewAsync();
        Task<TSharingItem> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TSharingItem>> GetAllForApiAsync();
        Task<TSharingItem> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }

    
}