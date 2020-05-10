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
    public class PizzaService :
        BaseEntityService<IAppUnitOfWork, IPizzaRepository, IPizzaServiceMapper, DAL.App.DTO.Pizza,
            BLL.App.DTO.Pizza>, IPizzaService
    {
        public PizzaService(IAppUnitOfWork uow) : 
            base(uow, uow.Pizzas, new PizzaServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<BLL.App.DTO.Pizza>> GetAllAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            return (await Repository.GetAllAsync(Id, userId, noTracking)).Select(e => Mapper.Map(e));
        }

    }
}