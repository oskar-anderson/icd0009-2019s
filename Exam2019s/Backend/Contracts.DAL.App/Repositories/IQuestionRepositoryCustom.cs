using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuestionRepositoryCustom : IQuestionRepositoryCustom<Question>
    {
        
    }
    
    public interface IQuestionRepositoryCustom<TQuestion>
    {
        Task<IEnumerable<TQuestion>> GetAllForViewAsync();
        Task<TQuestion> FirstOrDefaultViewAsync(Guid id, Guid? userId = null);
        Task<IEnumerable<TQuestion>> GetAllForApiAsync();
        Task<TQuestion> FirstOrDefaultApiAsync(Guid id, Guid? userId = null);

    }
}