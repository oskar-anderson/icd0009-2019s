using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;

namespace BLL.App.Services
{
    public class GpsSessionService :
        BaseEntityService<IAppUnitOfWork, IGpsSessionRepository, IGpsSessionServiceMapper, DAL.App.DTO.GpsSession,
            BLL.App.DTO.GpsSession>, IGpsSessionService
    {
        public GpsSessionService(IAppUnitOfWork uow) : base(uow, uow.GpsSessions, new GpsSessionServiceMapper())
        {
        }


        public virtual async Task<IEnumerable<GpsSessionView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapGpsSessionView(e));
        }
    }
}