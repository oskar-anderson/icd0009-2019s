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
        Task<IEnumerable<TUserLocation>> GetAllForViewAsync(Guid userId);
        Task<TUserLocation> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TUserLocation>> GetAllForApiAsync(Guid userId);
        Task<TUserLocation> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);
    }

    
}