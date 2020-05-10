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
    }
}