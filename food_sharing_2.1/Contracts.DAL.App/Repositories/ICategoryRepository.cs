using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> AllAsync(Guid? userId = null);
        Task<Category> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<CategoryDTO>> DTOAllAsync(Guid? userId = null);
        // Task<CategoryDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}