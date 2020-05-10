using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Base.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IMealRepositoryCustom: IMealRepositoryCustom<Meal>
    {
    }

    public interface IMealRepositoryCustom<TMeal>
    {
        Task<IEnumerable<TMeal>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}