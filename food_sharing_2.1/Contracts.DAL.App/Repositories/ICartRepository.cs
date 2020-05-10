using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartRepository : ICartRepository<Guid, Cart>, IBaseRepository<Cart>
    {
    }
    
    public interface ICartRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>, IComparable
    {
        Task<IEnumerable<Cart>> AllAsync(Guid? userId = null);
        Task<Cart> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        // Task<IEnumerable<CartDTO>> DTOAllAsync(Guid? userId = null);
        // Task<CartDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}