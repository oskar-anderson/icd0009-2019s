using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaFinalRepository : IBaseRepository<PizzaFinal>
    {
        Task<IEnumerable<PizzaFinal>> AllAsync(Guid? userId = null);
        Task<PizzaFinal> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<PizzaFinalDTO>> DTOAllAsync(Guid? userId = null);
        Task<PizzaFinalDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}