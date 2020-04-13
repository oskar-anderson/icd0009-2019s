using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaTemplateRepository : IBaseRepository<PizzaTemplate>
    {
        Task<IEnumerable<PizzaTemplate>> AllAsync(Guid? userId = null);
        Task<PizzaTemplate> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<PizzaTemplateDTO>> DTOAllAsync(Guid? userId = null);
        Task<PizzaTemplateDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}