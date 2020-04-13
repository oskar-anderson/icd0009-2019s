using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class MealRepository :  EFBaseRepository<Meal, AppDbContext>, IMealRepository
    {
        public MealRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Meal>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(m => m.Category)
                .AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<Meal> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(m => m.Category)
                .Where(m => m.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet
                .AnyAsync(m => m.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var meal = await FirstOrDefaultAsync(id, userId);
            base.Remove(meal);
        }

        public async Task<IEnumerable<MealDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            return await query
                .Select(m => new MealDTO()
                {
                    Id = m.Id,
                    CategoryId = m.CategoryId,
                    Category = new CategoryDTO()
                    {
                        Id = m.Category.Id,
                        Name = m.Category.Name,
                    },
                    Name = m.Name,
                    Picture = m.Picture,
                    Description = m.Description
                })
                .ToListAsync();
        }

        public async Task<MealDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(m => m.Id == id).AsQueryable();
            MealDTO mealDTO = await query.Select(m => new MealDTO()
            {
                Id = m.Id,
                CategoryId = m.CategoryId,
                Category = new CategoryDTO()
                {
                    Id = m.Category.Id,
                    Name = m.Category.Name,
                },
                Name = m.Name,
                Picture = m.Picture,
                Description = m.Description
            }).FirstOrDefaultAsync();

            return mealDTO;
        }
    }
}