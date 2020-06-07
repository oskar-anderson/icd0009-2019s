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
    public class SharingItemService : 
        BaseEntityService<IAppUnitOfWork, ISharingItemRepository, ISharingItemServiceMapper, DAL.App.DTO.SharingItem,
            BLL.App.DTO.SharingItem>, ISharingItemService
    {
        public SharingItemService(IAppUnitOfWork uow) : 
            base(uow, uow.SharingItems, new SharingItemServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<SharingItem>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapSharingItemView(e));
        }

        public virtual async Task<SharingItem> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapSharingItemView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<SharingItem>> GetAllForApiAsync()
        {
            return (await Repository.GetAllForApiAsync()).Select(e => Mapper.MapSharingItemView(e));
        }

        public virtual async Task<SharingItem> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapSharingItemView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}