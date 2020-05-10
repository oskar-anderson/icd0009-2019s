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
    public class CategoryRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Category, Category>, 
        ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.Category, Category>())
        {

        }

        public async Task<IEnumerable<Category>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Category> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(a => a.Id == id)
                .AsQueryable();
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var category = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(category.Id);
        }
        /*
        public async Task<IEnumerable<CategoryDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();
            return await query
                .Select(c => new CategoryDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<CategoryDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();
            var categoryDTO = query
                .Select(c => new CategoryDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .FirstOrDefaultAsync();
            return await categoryDTO;
        }
        */
    }
}