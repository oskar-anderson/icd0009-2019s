using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Base.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepositoryCustom: ICategoryRepositoryCustom<Category>
    {
    }

    public interface ICategoryRepositoryCustom<TCategory>
    {
        Task<IEnumerable<TCategory>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}