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
    public class SharingService : 
        BaseEntityService<IAppUnitOfWork, ISharingRepository, ISharingServiceMapper, DAL.App.DTO.Sharing,
            BLL.App.DTO.Sharing>, ISharingService
    {

        public SharingService(IAppUnitOfWork uow) : 
            base(uow, uow.Sharings, new SharingServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<Sharing>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapSharingView(e));
        }
    }
}