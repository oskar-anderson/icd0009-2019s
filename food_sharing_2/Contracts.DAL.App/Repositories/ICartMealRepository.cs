using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartMealRepository : IBaseRepository<CartMeal>
    {
        Task<IEnumerable<CartMeal>> AllAsync(Guid? userId = null);
        Task<CartMeal> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<CartMealDTO>> DTOAllAsync(Guid? userId = null);
        Task<CartMealDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}