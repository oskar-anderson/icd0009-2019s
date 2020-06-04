using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGpsLocationRepository  : IBaseRepository<GpsLocation>, IGpsLocationRepositoryCustom
    {
        Task<GpsLocation> LastInSessionAsync(Guid gpsSessionId, bool noTracking = true);
    }
}