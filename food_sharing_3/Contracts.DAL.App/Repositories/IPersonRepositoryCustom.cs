using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRepositoryCustom: IPersonRepositoryCustom<Person>
    {
    }

    public interface IPersonRepositoryCustom<TPerson>
    {
        Task<IEnumerable<TPerson>> GetAllForViewAsync();
    }

    
}