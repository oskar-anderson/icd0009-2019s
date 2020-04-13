using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<IEnumerable<Cart>> AllAsync(Guid? userId = null);
        Task<Cart> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<CartDTO>> DTOAllAsync(Guid? userId = null);
        Task<CartDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}