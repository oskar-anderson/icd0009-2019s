using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Base.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaRepositoryCustom: IPizzaRepositoryCustom<Pizza>
    {
    }

    public interface IPizzaRepositoryCustom<TPizza>
    {
        Task<IEnumerable<TPizza>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true);
    }

    
}