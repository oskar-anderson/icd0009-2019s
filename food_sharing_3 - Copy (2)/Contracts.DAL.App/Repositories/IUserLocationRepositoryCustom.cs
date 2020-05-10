using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserLocationRepositoryCustom: IUserLocationRepositoryCustom<UserLocation>
    {
    }

    public interface IUserLocationRepositoryCustom<TUserLocation>
    {
        Task<IEnumerable<TUserLocation>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}