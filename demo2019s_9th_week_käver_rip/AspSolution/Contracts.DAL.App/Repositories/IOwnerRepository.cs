using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IOwnerRepository : IBaseRepository<Owner>
    {
        Task<IEnumerable<Owner>> AllAsync(Guid? userId = null);
        Task<Owner> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        
        // DTO methods
        Task<IEnumerable<OwnerDTO>> DTOAllAsync(Guid? userId = null);
        Task<OwnerDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
        
    }
}