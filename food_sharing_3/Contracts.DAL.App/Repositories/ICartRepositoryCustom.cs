using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartRepositoryCustom: ICartRepositoryCustom<Cart>
    {
    }

    public interface ICartRepositoryCustom<TCart>
    {
        Task<IEnumerable<TCart>> GetAllForViewAsync(Guid userId);
        Task<TCart> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TCart>> GetAllForApiAsync(Guid userId);
        Task<TCart> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }
    
}