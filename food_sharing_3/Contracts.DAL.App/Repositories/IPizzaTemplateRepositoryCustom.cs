using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaTemplateRepositoryCustom: IPizzaTemplateRepositoryCustom<PizzaTemplate>
    {
    }

    public interface IPizzaTemplateRepositoryCustom<TPizzaTemplate>
    {
        Task<IEnumerable<TPizzaTemplate>> GetAllForViewAsync();
    }

    
}