﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Domain.Base.App.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MealRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Meal, Meal>, 
        IMealRepository
    {
        public MealRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.Meal, Meal>())
        {
        }

        public async Task<IEnumerable<Meal>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(m => m.Category)
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Meal> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(m => m.Category)
                .Where(m => m.Id == id)
                .AsQueryable();
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet
                .AnyAsync(m => m.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var meal = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(meal.Id);
        }
        
        /*
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
        */
    }
}