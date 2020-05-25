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
    public class ComponentPizzaUserService :
        BaseEntityService<IAppUnitOfWork, IComponentPizzaUserRepository, IComponentPizzaUserServiceMapper, DAL.App.DTO.ComponentPizzaUser,
            BLL.App.DTO.ComponentPizzaUser>, IComponentPizzaUserService
    {
        public ComponentPizzaUserService(IAppUnitOfWork uow) : 
            base(uow, uow.ComponentPizzaUsers, new ComponentPizzaUserServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<ComponentPizzaUser>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapComponentPizzaUserView(e));
        }

        public virtual async Task<ComponentPizzaUser> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapComponentPizzaUserView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<ComponentPizzaUser>> GetAllForApiAsync()
        {
            return (await Repository.GetAllForApiAsync()).Select(e => Mapper.MapComponentPizzaUserView(e));
        }

        public virtual async Task<ComponentPizzaUser> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapComponentPizzaUserView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}