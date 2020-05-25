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
        Task<IEnumerable<TCartMeal>> GetAllForViewAsync();
        Task<TCartMeal> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TCartMeal>> GetAllForApiAsync();
        Task<TCartMeal> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }
    
}