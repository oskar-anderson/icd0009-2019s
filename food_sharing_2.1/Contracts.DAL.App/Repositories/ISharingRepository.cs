using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface ISharingRepository : IBaseRepository<Sharing>
    {
        Task<IEnumerable<Sharing>> AllAsync(Guid? userId = null);
        Task<Sharing> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<SharingDTO>> DTOAllAsync(Guid? userId = null);
        // Task<SharingDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}