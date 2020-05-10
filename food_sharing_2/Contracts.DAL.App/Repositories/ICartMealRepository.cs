using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Domain.Base.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartMealRepository  : IBaseRepository<CartMeal>, ICartMealRepositoryCustom
    {
    }
}