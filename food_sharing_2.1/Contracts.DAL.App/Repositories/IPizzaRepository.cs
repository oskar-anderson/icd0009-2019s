using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaRepository : IBaseRepository<Pizza>
    {
        Task<IEnumerable<Pizza>> AllAsync(Guid? userId = null);
        Task<Pizza> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<PizzaDTO>> DTOAllAsync(Guid? userId = null);
        // Task<PizzaDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}