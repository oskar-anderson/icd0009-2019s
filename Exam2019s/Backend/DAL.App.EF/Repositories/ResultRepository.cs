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
    public class ResultRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Result, Result>, 
        IResultRepository
    {
        public ResultRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.Result, Result>())
        {
        }
        
        public virtual async Task<IEnumerable<DAL.App.DTO.Result>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(c => new DAL.App.DTO.Result()
                {
                    Id = c.Id,
                    Score = c.Score,
                    QuizId = c.QuizId,
                    Quiz = new Quiz()
                    {
                        AppUserId = c.Quiz.AppUserId,
                        Name = c.Quiz.Name,
                        Description = c.Quiz.Description,
                    },
                    QuestionToPickedAnswer = c.QuestionToPickedAnswer,
                    PersonName = c.PersonName,
                })
                .ToListAsync();
        }

        public virtual async Task<Result> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Result()
                {
                    Id = c.Id,
                    Score = c.Score,
                    QuizId = c.QuizId,
                    Quiz = new Quiz()
                    {
                        AppUserId = c.Quiz.AppUserId,
                        Name = c.Quiz.Name,
                        Description = c.Quiz.Description,
                    },
                    QuestionToPickedAnswer = c.QuestionToPickedAnswer,
                    PersonName = c.PersonName,
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<Result>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(c => new DAL.App.DTO.Result()
                {
                    Id = c.Id,
                    Score = c.Score,
                    QuizId = c.QuizId,
                    Quiz = new Quiz()
                    {
                        AppUserId = c.Quiz.AppUserId,
                        Name = c.Quiz.Name,
                        Description = c.Quiz.Description,
                    },
                    QuestionToPickedAnswer = c.QuestionToPickedAnswer,
                    PersonName = c.PersonName,
                })
                .ToListAsync();
        }

        public virtual async Task<Result> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Result()
                {
                    Id = c.Id,
                    Score = c.Score,
                    QuizId = c.QuizId,
                    Quiz = new Quiz()
                    {
                        AppUserId = c.Quiz.AppUserId,
                        Name = c.Quiz.Name,
                        Description = c.Quiz.Description,
                    },
                    QuestionToPickedAnswer = c.QuestionToPickedAnswer,
                    PersonName = c.PersonName,
                })
                .FirstOrDefaultAsync();
        }
    }
}