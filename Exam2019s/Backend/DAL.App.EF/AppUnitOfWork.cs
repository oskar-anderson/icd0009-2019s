using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }


        public IChoiceRepository Choices =>
            GetRepository<IChoiceRepository>(() => new ChoiceRepository(UOWDbContext));

        public IQuestionRepository Questions =>
            GetRepository<IQuestionRepository>(() => new QuestionRepository(UOWDbContext));

        public IQuizRepository Quizzes =>
            GetRepository<IQuizRepository>(() => new QuizRepository(UOWDbContext));

        public IResultRepository Results =>
            GetRepository<IResultRepository>(() => new ResultRepository(UOWDbContext));
    }
}