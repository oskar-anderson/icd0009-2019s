using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartMealRepository : IBaseRepository<Guid, CartMeal>
    {
    
        Task<IEnumerable<CartMeal>> AllAsync(Guid? userId = null);
        Task<CartMeal> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<CartMealDTO>> DTOAllAsync(Guid? userId = null);
        // Task<CartMealDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}