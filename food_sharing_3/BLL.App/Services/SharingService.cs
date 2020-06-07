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
    public class SharingService : 
        BaseEntityService<IAppUnitOfWork, ISharingRepository, ISharingServiceMapper, DAL.App.DTO.Sharing,
            BLL.App.DTO.Sharing>, ISharingService
    {

        public SharingService(IAppUnitOfWork uow) : 
            base(uow, uow.Sharings, new SharingServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<Sharing>> GetAllForViewAsync(Guid userId)
        {
            return (await Repository.GetAllForViewAsync(userId)).Select(e => Mapper.MapSharingView(e));
        }

        public virtual async Task<Sharing> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapSharingView(await Repository.FirstOrDefaultViewAsync(id, userId));
        }

        public virtual async Task<IEnumerable<Sharing>> GetAllForApiAsync(Guid userId)
        {
            return (await Repository.GetAllForApiAsync(userId)).Select(e => Mapper.MapSharingView(e));
        }

        public virtual async Task<Sharing> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            return Mapper.MapSharingView(await Repository.FirstOrDefaultApiAsync(id, userId));
        }
    }
}