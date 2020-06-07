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

        public virtual async Task<Item> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapItemView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<Item>> GetAllForApiAsync()
        {
            return (await Repository.GetAllForApiAsync()).Select(e => Mapper.MapItemView(e));
        }

        public virtual async Task<Item> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapItemView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}