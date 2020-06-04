using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IGpsLocationService : IBaseEntityService<GpsLocation>, IGpsLocationRepositoryCustom<GpsLocation>
    {
        Task<GpsLocation> AddAndUpdateSessionAsync(GpsLocation gpsLocation);
    }
}