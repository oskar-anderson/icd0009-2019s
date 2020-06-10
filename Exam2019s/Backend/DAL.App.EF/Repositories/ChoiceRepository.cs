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
    public class ChoiceRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Choice, Choice>, 
        IChoiceRepository
    {
        public ChoiceRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.Choice, Choice>())
        {
        }
        
        public virtual async Task<IEnumerable<DAL.App.DTO.Choice>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(c => new DAL.App.DTO.Choice()
                {
                    Id = c.Id,
                    Name = c.Name,
                    GivesPoints = c.GivesPoints,
                    QuestionId = c.QuestionId,
                    Question = new Question()
                    {
                        OrderNumber = c.Question.OrderNumber,
                        QuestionName = c.Question.QuestionName,
                        Points = c.Question.Points,
                        QuizId = c.Question.QuizId,
                        Quiz = new Quiz()
                        {
                            AppUserId = c.Question.Quiz.AppUserId,
                            Name = c.Question.Quiz.Name,
                            Description = c.Question.Quiz.Description,
                        }
                    },
                })
                .ToListAsync();
        }

        public virtual async Task<Choice> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Choice()
                {
                    Id = c.Id,
                    Name = c.Name,
                    GivesPoints = c.GivesPoints,
                    QuestionId = c.QuestionId,
                    Question = new Question()
                    {
                        OrderNumber = c.Question.OrderNumber,
                        QuestionName = c.Question.QuestionName,
                        Points = c.Question.Points,
                        QuizId = c.Question.QuizId,
                        Quiz = new Quiz()
                        {
                            AppUserId = c.Question.Quiz.AppUserId,
                            Name = c.Question.Quiz.Name,
                            Description = c.Question.Quiz.Description,
                        }
                    },
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<Choice>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(c => new DAL.App.DTO.Choice()
                {
                    Id = c.Id,
                    Name = c.Name,
                    GivesPoints = c.GivesPoints,
                    QuestionId = c.QuestionId,
                    Question = new Question()
                    {
                        OrderNumber = c.Question.OrderNumber,
                        QuestionName = c.Question.QuestionName,
                        Points = c.Question.Points,
                        QuizId = c.Question.QuizId,
                        Quiz = new Quiz()
                        {
                            AppUserId = c.Question.Quiz.AppUserId,
                            Name = c.Question.Quiz.Name,
                            Description = c.Question.Quiz.Description,
                        }
                    },
                })
                .ToListAsync();
        }

        public virtual async Task<Choice> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Choice()
                {
                    Id = c.Id,
                    Name = c.Name,
                    GivesPoints = c.GivesPoints,
                    QuestionId = c.QuestionId,
                    Question = new Question()
                    {
                        OrderNumber = c.Question.OrderNumber,
                        QuestionName = c.Question.QuestionName,
                        Points = c.Question.Points,
                        QuizId = c.Question.QuizId,
                        Quiz = new Quiz()
                        {
                            AppUserId = c.Question.Quiz.AppUserId,
                            Name = c.Question.Quiz.Name,
                            Description = c.Question.Quiz.Description,
                        }
                    },
                })
                .FirstOrDefaultAsync();
        }
        
    }
}