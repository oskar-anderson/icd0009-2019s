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
    public class PizzaFinalService : 
        BaseEntityService<IAppUnitOfWork, IPizzaFinalRepository, IPizzaFinalServiceMapper, DAL.App.DTO.PizzaFinal,
            BLL.App.DTO.PizzaFinal>, IPizzaFinalService
    {
        public PizzaFinalService(IAppUnitOfWork uow) : 
            base(uow, uow.PizzaFinals, new PizzaFinalServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<PizzaFinal>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapPizzaFinalView(e));
        }
    }
}