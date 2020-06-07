using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using ee.itcollege.kaande.pitsariina.BLL.Base.Services;

namespace BLL.App.Services
{
    public class ComponentPizzaTemplateService :
        BaseEntityService<IAppUnitOfWork, IComponentPizzaTemplateRepository, IComponentPizzaTemplateServiceMapper, DAL.App.DTO.ComponentPizzaTemplate,
            BLL.App.DTO.ComponentPizzaTemplate>, IComponentPizzaTemplateService
    {
        public ComponentPizzaTemplateService(IAppUnitOfWork uow) : 
            base(uow, uow.ComponentPizzaTemplates, new ComponentPizzaTemplateServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<ComponentPizzaTemplate>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapComponentPizzaTemplateView(e));
        }

        public virtual async Task<ComponentPizzaTemplate> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapComponentPizzaTemplateView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<ComponentPizzaTemplate>> GetAllForApiAsync()
        {
            return (await Repository.GetAllForApiAsync()).Select(e => Mapper.MapComponentPizzaTemplateView(e));
        }

        public virtual async Task<ComponentPizzaTemplate> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapComponentPizzaTemplateView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}