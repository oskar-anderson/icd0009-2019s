using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRestaurantFoodRepositoryCustom: IRestaurantFoodRepositoryCustom<RestaurantFood>
    {
    }

    public interface IRestaurantFoodRepositoryCustom<TRestaurantFood>
    {
        Task<IEnumerable<TRestaurantFood>> GetAllForViewAsync();
        Task<TRestaurantFood> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TRestaurantFood>> GetAllForApiAsync();
        Task<TRestaurantFood> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }

    
}