using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.kaande.pitsariina.BLL.Base.Services;


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

        public virtual async Task<CartMeal> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapCartMealView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<CartMeal>> GetAllForApiAsync()
        {
            return (await Repository.GetAllForApiAsync()).Select(e => Mapper.MapCartMealView(e));
        }

        public virtual async Task<CartMeal> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapCartMealView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}