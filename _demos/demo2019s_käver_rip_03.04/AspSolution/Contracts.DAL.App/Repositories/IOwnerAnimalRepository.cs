using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IOwnerAnimalRepository : IBaseRepository<OwnerAnimal>
    {
        Task<IEnumerable<OwnerAnimal>> AllAsync(Guid? userId = null);
        Task<OwnerAnimal> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<OwnerAnimalDTO>> DTOAllAsync(Guid? userId = null);
        Task<OwnerAnimalDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);

    }
}