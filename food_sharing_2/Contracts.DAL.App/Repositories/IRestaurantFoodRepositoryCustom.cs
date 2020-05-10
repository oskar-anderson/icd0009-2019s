﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Base.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRestaurantFoodRepositoryCustom: IRestaurantFoodRepositoryCustom<RestaurantFood>
    {
    }

    public interface IRestaurantFoodRepositoryCustom<TRestaurantFood>
    {
        Task<IEnumerable<TRestaurantFood>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}