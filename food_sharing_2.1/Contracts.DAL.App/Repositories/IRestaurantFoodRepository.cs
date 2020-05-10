using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRestaurantFoodRepository : IBaseRepository<RestaurantFood>
    {
        Task<IEnumerable<RestaurantFood>> AllAsync(Guid? userId = null);
        Task<RestaurantFood> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<RestaurantFoodDTO>> DTOAllAsync(Guid? userId = null);
        // Task<RestaurantFoodDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}