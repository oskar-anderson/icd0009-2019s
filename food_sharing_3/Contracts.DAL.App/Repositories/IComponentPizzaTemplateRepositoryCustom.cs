using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IComponentPizzaTemplateRepositoryCustom: IComponentPizzaTemplateRepositoryCustom<ComponentPizzaTemplate>
    {
    }

    public interface IComponentPizzaTemplateRepositoryCustom<TComponentPizzaTemplate>
    {
        Task<IEnumerable<TComponentPizzaTemplate>> GetAllForViewAsync();
        Task<TComponentPizzaTemplate> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TComponentPizzaTemplate>> GetAllForApiAsync();
        Task<TComponentPizzaTemplate> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }

    
}