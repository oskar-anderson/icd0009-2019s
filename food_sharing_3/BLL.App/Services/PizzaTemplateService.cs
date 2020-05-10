using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

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

    }
}