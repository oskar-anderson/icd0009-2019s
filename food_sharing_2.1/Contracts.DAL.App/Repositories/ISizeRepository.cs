using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ISizeRepository : IBaseRepository<Size>
    {
        Task<IEnumerable<Size>> AllAsync(Guid? userId = null);
        Task<Size> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<SizeDTO>> DTOAllAsync(Guid? userId = null);
        // Task<SizeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}