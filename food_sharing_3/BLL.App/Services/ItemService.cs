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
    public class ItemService : 
        BaseEntityService<IAppUnitOfWork, IItemRepository, IItemServiceMapper, DAL.App.DTO.Item,
            BLL.App.DTO.Item>, IItemService
    {
        public ItemService(IAppUnitOfWork uow) : 
            base(uow, uow.Items, new ItemServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<Item>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapItemView(e));
        }
    }
}