using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IComponentRepositoryCustom: IComponentRepositoryCustom<Component>
    {
    }

    public interface IComponentRepositoryCustom<TComponent>
    {
        Task<IEnumerable<TComponent>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}