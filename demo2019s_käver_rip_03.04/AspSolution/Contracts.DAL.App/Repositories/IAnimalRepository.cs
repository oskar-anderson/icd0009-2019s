using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IAnimalRepository : IBaseRepository<Animal>
    {
        Task<IEnumerable<Animal>> AllAsync(Guid? userId = null);
        Task<Animal> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<AnimalDTO>> DTOAllAsync(Guid? userId = null);
        Task<AnimalDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}