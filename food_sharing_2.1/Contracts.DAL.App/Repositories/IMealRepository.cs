using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IMealRepository : IBaseRepository<Meal>
    {
        Task<IEnumerable<Meal>> AllAsync(Guid? userId = null);
        Task<Meal> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<MealDTO>> DTOAllAsync(Guid? userId = null);
        // Task<MealDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}