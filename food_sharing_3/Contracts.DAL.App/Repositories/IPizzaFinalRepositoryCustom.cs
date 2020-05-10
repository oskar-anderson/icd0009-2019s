using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaFinalRepositoryCustom: IPizzaFinalRepositoryCustom<PizzaFinal>
    {
    }

    public interface IPizzaFinalRepositoryCustom<TPizzaFinal>
    {
        Task<IEnumerable<TPizzaFinal>> GetAllForViewAsync();
    }

    
}