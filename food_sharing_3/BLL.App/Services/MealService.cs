using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class MealService :
        BaseEntityService<IAppUnitOfWork, IMealRepository, IMealServiceMapper, DAL.App.DTO.Meal,
            BLL.App.DTO.Meal>, IMealService
    {
        public MealService(IAppUnitOfWork uow) : 
            base(uow, uow.Meals, new MealServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<Meal>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapMealView(e));
        }
    }
}