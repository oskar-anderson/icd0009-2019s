using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IChoiceRepository Choices { get; }
        IQuestionRepository Questions { get; }
        IQuizRepository Quizzes { get; }
        IResultRepository Results { get; }
    }
}