using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Domain.Base.App.DTO;
using Domain.Base.EF.Repositories;
using Domain.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Base.App.EF.Repositories
{
    public class CategoryRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Category, DTO.Category>, 
        ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.Category, DTO.Category>())
        {

        }

        public async Task<IEnumerable<DTO.Category>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.Category> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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