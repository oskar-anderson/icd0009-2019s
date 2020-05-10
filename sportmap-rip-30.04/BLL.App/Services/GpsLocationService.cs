using System;
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

namespace BLL.App.Services
{
    public class GpsLocationService :
        BaseEntityService<IAppUnitOfWork, IGpsLocationRepository, IGpsLocationServiceMapper, DAL.App.DTO.GpsLocation,
            BLL.App.DTO.GpsLocation>, IGpsLocationService
    {
        public GpsLocationService(IAppUnitOfWork uow) : base(uow, uow.GpsLocations, new GpsLocationServiceMapper())
        {
        }

        public virtual async Task<IEnumerable<GpsLocation>> GetAllAsync(Guid gpsSessionId, Guid? userId = null,
            bool noTracking = true)
        {
            return (await Repository.GetAllAsync(gpsSessionId, userId, noTracking)).Select(e => Mapper.Map(e));
        }
    }
}