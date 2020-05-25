using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IComponentPizzaUserRepositoryCustom: IComponentPizzaUserRepositoryCustom<ComponentPizzaUser>
    {
    }

    public interface IComponentPizzaUserRepositoryCustom<TComponentPizzaUser>
    {
        Task<IEnumerable<TComponentPizzaUser>> GetAllForViewAsync();
        Task<TComponentPizzaUser> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TComponentPizzaUser>> GetAllForApiAsync();
        Task<TComponentPizzaUser> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }

    
}