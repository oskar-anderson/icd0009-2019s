using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
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


        public virtual async Task<IEnumerable<PizzaComponent>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapPizzaComponentView(e));
        }
    }
}