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
    }

    
}