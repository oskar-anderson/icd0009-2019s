using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PizzaTemplateService : 
        BaseEntityService<IAppUnitOfWork, IPizzaTemplateRepository, IPizzaTemplateServiceMapper, Domain.Base.App.DTO.PizzaTemplate,
            BLL.App.DTO.PizzaTemplate>, IPizzaTemplateService
    {
        public PizzaTemplateService(IAppUnitOfWork uow) : 
            base(uow, uow.PizzaTemplates, new PizzaTemplateServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<BLL.App.DTO.PizzaTemplate>> GetAllAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            return (await Repository.GetAllAsyncSpecific_DALAppEF_BLLBase(Id, userId, noTracking)).Select(e => Mapper.Map(e));
        }

    }
}