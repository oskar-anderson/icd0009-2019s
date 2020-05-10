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
    public class RestaurantFoodService : 
        BaseEntityService<IAppUnitOfWork, IRestaurantFoodRepository, IRestaurantFoodServiceMapper, DAL.App.DTO.RestaurantFood,
            BLL.App.DTO.RestaurantFood>, IRestaurantFoodService
    {
        public RestaurantFoodService(IAppUnitOfWork uow) : 
            base(uow, uow.RestaurantFoods, new RestaurantFoodServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<RestaurantFood>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapRestaurantFoodView(e));
        }
    }
}