using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaComponentRepositoryCustom: IPizzaComponentRepositoryCustom<PizzaComponent>
    {
    }

    public interface IPizzaComponentRepositoryCustom<TPizzaComponent>
    {
        Task<IEnumerable<TPizzaComponent>> GetAllForViewAsync();
    }

    
}