using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGpsLocationRepositoryCustom: IGpsLocationRepositoryCustom<GpsLocation>
    {
    }

    public interface IGpsLocationRepositoryCustom<TGpsLocation>
    {
        Task<IEnumerable<TGpsLocation>> GetAllAsync(Guid gpsSessionId, Guid? userId = null, bool noTracking = true);
    }
    
}