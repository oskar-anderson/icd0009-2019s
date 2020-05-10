using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartMealRepositoryCustom: ICartMealRepositoryCustom<CartMeal>
    {
    }

    public interface ICartMealRepositoryCustom<TCartMeal>
    {
        Task<IEnumerable<TCartMeal>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }
    
}