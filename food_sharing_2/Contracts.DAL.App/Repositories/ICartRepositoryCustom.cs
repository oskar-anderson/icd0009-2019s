using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Base.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICartRepositoryCustom: ICartRepositoryCustom<Cart>
    {
    }

    public interface ICartRepositoryCustom<TCart>
    {
        Task<IEnumerable<TCart>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }
    
}