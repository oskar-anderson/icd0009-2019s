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
    public class ComponentPriceService : 
        BaseEntityService<IAppUnitOfWork, IComponentPriceRepository, IComponentPriceServiceMapper, DAL.App.DTO.ComponentPrice,
        BLL.App.DTO.ComponentPrice>, IComponentPriceService
    {
        public ComponentPriceService(IAppUnitOfWork uow) : 
            base(uow, uow.ComponentPrices, new ComponentPriceServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<ComponentPrice>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapComponentPriceView(e));
        }
    }
}