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
    public class PizzaComponentService :
        BaseEntityService<IAppUnitOfWork, IPizzaComponentRepository, IPizzaComponentServiceMapper, DAL.App.DTO.PizzaComponent,
            BLL.App.DTO.PizzaComponent>, IPizzaComponentService
    {
        public PizzaComponentService(IAppUnitOfWork uow) : 
            base(uow, uow.PizzaComponents, new PizzaComponentServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<BLL.App.DTO.PizzaComponent>> GetAllAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            return (await Repository.GetAllAsync(Id, userId, noTracking)).Select(e => Mapper.Map(e));
        }

    }
}