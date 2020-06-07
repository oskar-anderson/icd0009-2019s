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
    public class PizzaTemplateService : 
        BaseEntityService<IAppUnitOfWork, IPizzaTemplateRepository, IPizzaTemplateServiceMapper, DAL.App.DTO.PizzaTemplate,
            BLL.App.DTO.PizzaTemplate>, IPizzaTemplateService
    {
        public PizzaTemplateService(IAppUnitOfWork uow) : 
            base(uow, uow.PizzaTemplates, new PizzaTemplateServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<PizzaTemplate>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapPizzaTemplateView(e));
        }
        public virtual async Task<PizzaTemplate> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapPizzaTemplateView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }
        public virtual async Task<IEnumerable<PizzaTemplate>> GetAllForApiAsync()
        {
            return (await Repository.GetAllForApiAsync()).Select(e => Mapper.MapPizzaTemplateView(e));
        }

        public virtual async Task<PizzaTemplate> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapPizzaTemplateView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
        
    }
}