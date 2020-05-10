using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        Task<IEnumerable<Restaurant>> AllAsync(Guid? userId = null);
        Task<Restaurant> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<RestaurantDTO>> DTOAllAsync(Guid? userId = null);
        // Task<RestaurantDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}