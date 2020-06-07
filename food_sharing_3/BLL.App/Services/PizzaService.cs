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
    public class PizzaService :
        BaseEntityService<IAppUnitOfWork, IPizzaRepository, IPizzaServiceMapper, DAL.App.DTO.Pizza,
            BLL.App.DTO.Pizza>, IPizzaService
    {
        public PizzaService(IAppUnitOfWork uow) : 
            base(uow, uow.Pizzas, new PizzaServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<Pizza>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapPizzaView(e));
        }

        public virtual async Task<Pizza> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapPizzaView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<Pizza>> GetAllForApiAsync()
        {
            return (await Repository.GetAllForApiAsync()).Select(e => Mapper.MapPizzaView(e));
        }

        public virtual async Task<Pizza> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapPizzaView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}