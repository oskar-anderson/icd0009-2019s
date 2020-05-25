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
        Task<IEnumerable<TSharing>> GetAllForViewAsync(Guid userId);
        Task<TSharing> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TSharing>> GetAllForApiAsync(Guid userId);
        Task<TSharing> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }

    
}