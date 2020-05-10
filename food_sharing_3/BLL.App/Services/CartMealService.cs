using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class CartMealService :
        BaseEntityService<IAppUnitOfWork, ICartMealRepository, ICartMealServiceMapper, DAL.App.DTO.CartMeal,
            BLL.App.DTO.CartMeal>, ICartMealService
    {
        public CartMealService(IAppUnitOfWork uow) : 
            base(uow, uow.CartMeals, new CartMealServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<CartMeal>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapCartMealView(e));
        }
    }
}