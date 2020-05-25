using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaRepositoryCustom: IPizzaRepositoryCustom<Pizza>
    {
    }

    public interface IPizzaRepositoryCustom<TPizza>
    {
        Task<IEnumerable<TPizza>> GetAllForViewAsync();
        Task<TPizza> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TPizza>> GetAllForApiAsync();
        Task<TPizza> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);
    }

    
}