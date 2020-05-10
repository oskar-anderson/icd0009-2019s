using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserLocationRepository : IBaseRepository<UserLocation>
    {
        Task<IEnumerable<UserLocation>> AllAsync(Guid? userId = null);
        Task<UserLocation> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<UserLocationDTO>> DTOAllAsync(Guid? userId = null);
        // Task<UserLocationDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}