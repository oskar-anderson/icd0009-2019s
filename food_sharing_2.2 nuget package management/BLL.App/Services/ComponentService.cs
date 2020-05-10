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
using DAL.App.DTO;


namespace BLL.App.Services
{
    public class ComponentService : 
        BaseEntityService<IAppUnitOfWork, IComponentRepository, IComponentServiceMapper, Component,
        BLL.App.DTO.Component>, IComponentService
    {
        public ComponentService(IAppUnitOfWork uow) : 
            base(uow, uow.Components, new ComponentServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<BLL.App.DTO.Component>> GetAllAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            return (await Repository.GetAllAsync(Id, userId, noTracking)).Select(e => Mapper.Map(e));
        }
        
    }
}