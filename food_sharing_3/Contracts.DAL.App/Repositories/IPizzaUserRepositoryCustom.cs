using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaUserRepositoryCustom: IPizzaUserRepositoryCustom<PizzaUser>
    {
    }

    public interface IPizzaUserRepositoryCustom<TPizzaUser>
    {
        Task<IEnumerable<TPizzaUser>> GetAllForViewAsync(Guid userId);
        Task<TPizzaUser> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TPizzaUser>> GetAllForApiAsync(Guid userId);
        Task<TPizzaUser> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }

    
}