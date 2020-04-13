using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IComponentRepository : IBaseRepository<Component>
    {
        Task<IEnumerable<Component>> AllAsync(Guid? userId = null);
        Task<Component> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<ComponentDTO>> DTOAllAsync(Guid? userId = null);
        Task<ComponentDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}