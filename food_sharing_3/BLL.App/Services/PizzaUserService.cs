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
    public class PizzaUserService : 
        BaseEntityService<IAppUnitOfWork, IPizzaUserRepository, IPizzaUserServiceMapper, DAL.App.DTO.PizzaUser,
            BLL.App.DTO.PizzaUser>, IPizzaUserService
    {
        public PizzaUserService(IAppUnitOfWork uow) : 
            base(uow, uow.PizzaUsers, new PizzaUserServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<PizzaUser>> GetAllForViewAsync(Guid userId)
        {
            return (await Repository.GetAllForViewAsync(userId)).Select(e => Mapper.MapPizzaFinalView(e));
        }

        public virtual async Task<PizzaUser> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapPizzaFinalView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<PizzaUser>> GetAllForApiAsync(Guid userId)
        {
            return (await Repository.GetAllForApiAsync(userId)).Select(e => Mapper.MapPizzaFinalView(e));
        }

        public virtual async Task<PizzaUser> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapPizzaFinalView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}