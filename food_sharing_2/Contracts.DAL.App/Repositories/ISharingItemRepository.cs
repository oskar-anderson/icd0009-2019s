using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;
using SharingItem = Domain.SharingItem;

namespace Contracts.DAL.App.Repositories
{
    public interface ISharingItemRepository : IBaseRepository<SharingItem>
    {
        Task<IEnumerable<SharingItem>> AllAsync(Guid? userId = null);
        Task<SharingItem> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<SharingItemDTO>> DTOAllAsync(Guid? userId = null);
        Task<SharingItemDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}