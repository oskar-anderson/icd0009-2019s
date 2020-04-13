using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaComponentRepository : IBaseRepository<PizzaComponent>
    {
        Task<IEnumerable<PizzaComponent>> AllAsync(Guid? userId = null);
        Task<PizzaComponent> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<PizzaComponentDTO>> DTOAllAsync(Guid? userId = null);
        Task<PizzaComponentDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}