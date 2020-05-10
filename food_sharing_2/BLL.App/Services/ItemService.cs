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
    public class ItemService : 
        BaseEntityService<IAppUnitOfWork, IItemRepository, IItemServiceMapper, Domain.Base.App.DTO.Item,
            BLL.App.DTO.Item>, IItemService
    {
        public ItemService(IAppUnitOfWork uow) : 
            base(uow, uow.Items, new ItemServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<BLL.App.DTO.Item>> GetAllAsync(Guid Id, Guid? userId = null,
            bool noTracking = true)
        {
            return (await Repository.GetAllAsync(Id, userId, noTracking)).Select(e => Mapper.Map(e));
        }
    }
}