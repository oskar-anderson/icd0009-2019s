using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRestaurantRepositoryCustom: IRestaurantRepositoryCustom<Restaurant>
    {
    }

    public interface IRestaurantRepositoryCustom<TRestaurant>
    {
    }

    
}