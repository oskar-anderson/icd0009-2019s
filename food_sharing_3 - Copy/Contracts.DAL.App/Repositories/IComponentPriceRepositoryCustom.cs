using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IComponentPriceRepositoryCustom: IComponentPriceRepositoryCustom<ComponentPrice>
    {
    }

    public interface IComponentPriceRepositoryCustom<TComponentPrice>
    {
        Task<IEnumerable<TComponentPrice>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}