using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuestionRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Question, Question>, 
        IQuestionRepository
    {
        public QuestionRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.Question, Question>())
        {
        }
        
        public virtual async Task<IEnumerable<DAL.App.DTO.Question>> GetAllForViewAsync()
        {
            List<Question> questions = await RepoDbSet
                .Select(c => new DAL.App.DTO.Question()
                {
                    Id = c.Id,
                    OrderNumber = c.OrderNumber,
                    QuestionName = c.QuestionName,
                    Points = c.Points,
                    QuizId = c.QuizId,
                    Quiz = new Quiz()
                    {
                        AppUserId = c.Quiz.AppUserId,
                        Name = c.Quiz.Name,
                        Description = c.Quiz.Description,
                    }
                })
                .ToListAsync();
            return questions.OrderBy(x => x.OrderNumber);;
        }
        

        public virtual async Task<Question> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Question()
                {
                    Id = c.Id,
                    OrderNumber = c.OrderNumber,
                    QuestionName = c.QuestionName,
                    Points = c.Points,
                    QuizId = c.QuizId,
                    Quiz = new Quiz()
                    {
                        AppUserId = c.Quiz.AppUserId,
                        Name = c.Quiz.Name,
                        Description = c.Quiz.Description,
                    }

                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<Question>> GetAllForApiAsync()
        {
            List<Question> questions =  await RepoDbSet
                .Select(c => new DAL.App.DTO.Question()
                {
                    Id = c.Id,
                    OrderNumber = c.OrderNumber,
                    QuestionName = c.QuestionName,
                    Points = c.Points,
                    QuizId = c.QuizId,
                    Quiz = new Quiz()
                    {
                        AppUserId = c.Quiz.AppUserId,
                        Name = c.Quiz.Name,
                        Description = c.Quiz.Description,
                    }
                })
                .ToListAsync();
            return questions.OrderBy(x => x.OrderNumber);;
        }

        public virtual async Task<Question> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Question()
                {
                    Id = c.Id,
                    OrderNumber = c.OrderNumber,
                    QuestionName = c.QuestionName,
                    Points = c.Points,
                    QuizId = c.QuizId,
                    Quiz = new Quiz()
                    {
                        AppUserId = c.Quiz.AppUserId,
                        Name = c.Quiz.Name,
                        Description = c.Quiz.Description,
                    }
                })
                .FirstOrDefaultAsync();
        }
    }
}