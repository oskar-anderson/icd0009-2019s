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
    public class QuizRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Quiz, Quiz>, 
        IQuizRepository
    {
        public QuizRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.Quiz, Quiz>())
        {
        }
        
        public virtual async Task<IEnumerable<DAL.App.DTO.Quiz>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(c => new DAL.App.DTO.Quiz()
                {
                    Id = c.Id,
                    AppUserId = c.AppUserId,
                    AppUser = c.AppUser,
                    Name = c.Name,
                    Description = c.Description,
                })
                .ToListAsync();
        }

        public virtual async Task<Quiz> FirstOrDefaultViewAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Quiz()
                {
                    Id = c.Id,
                    AppUserId = c.AppUserId,
                    AppUser = c.AppUser,
                    Name = c.Name,
                    Description = c.Description,
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<Quiz>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(c => new DAL.App.DTO.Quiz()
                {
                    Id = c.Id,
                    AppUserId = c.AppUserId,
                    Name = c.Name,
                    Description = c.Description,
                })
                .ToListAsync();
        }

        public virtual async Task<Quiz> FirstOrDefaultApiAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Quiz()
                {
                    Id = c.Id,
                    AppUserId = c.AppUserId,
                    Name = c.Name,
                    Description = c.Description,
                })
                .FirstOrDefaultAsync();
        }
    }
}