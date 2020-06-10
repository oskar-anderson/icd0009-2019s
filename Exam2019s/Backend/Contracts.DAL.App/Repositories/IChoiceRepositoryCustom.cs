using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IChoiceRepositoryCustom :  IChoiceRepositoryCustom<Choice>
    {

    }
    
    public interface IChoiceRepositoryCustom<TChoice>
    {
        Task<IEnumerable<TChoice>> GetAllForViewAsync();
        Task<TChoice> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TChoice>> GetAllForApiAsync();
        Task<TChoice> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }
}