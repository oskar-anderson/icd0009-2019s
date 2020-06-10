using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuizRepositoryCustom : IQuizRepositoryCustom<Quiz>
    {
        
    }
    
    public interface IQuizRepositoryCustom<TQuiz>
    {
        Task<IEnumerable<TQuiz>> GetAllForViewAsync();
        Task<TQuiz> FirstOrDefaultViewAsync(Guid id);
        Task<IEnumerable<TQuiz>> GetAllForApiAsync();
        Task<TQuiz> FirstOrDefaultApiAsync(Guid id);

    }
}