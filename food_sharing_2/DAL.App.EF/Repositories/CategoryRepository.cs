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
    public class CategoryRepository : EFBaseRepository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Category>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();
            return await query.ToListAsync();
        }

        public async Task<Category> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(a => a.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var delivery = await FirstOrDefaultAsync(id, userId);
            base.Remove(delivery);
        }

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
    }
}