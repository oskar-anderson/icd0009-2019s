using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ISharingRepositoryCustom: ISharingRepositoryCustom<Sharing>
    {
    }

    public interface ISharingRepositoryCustom<TSharing>
    {
        Task<IEnumerable<TSharing>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}