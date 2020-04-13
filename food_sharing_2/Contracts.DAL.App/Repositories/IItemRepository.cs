using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        Task<IEnumerable<Item>> AllAsync(Guid? userId = null);
        Task<Item> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<ItemDTO>> DTOAllAsync(Guid? userId = null);
        Task<ItemDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}