using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGpsSessionRepository : IBaseRepository<GpsSession>, IGpsSessionRepositoryCustom
    {
        Task<GpsSession> GetFirstWithAllLocationsAsync(Guid id);
    }
}