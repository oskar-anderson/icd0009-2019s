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
    public class ComponentRepository :  EFBaseRepository<Component, AppDbContext>, IComponentRepository
    {
        public ComponentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Component>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<Component> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(cm => cm.Id == id)
                .AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(cm => cm.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var component = await FirstOrDefaultAsync(id, userId);
            base.Remove(component);
        }

        public async Task<IEnumerable<ComponentDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();

            return await query
                .Select(c => new ComponentDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Max = c.Max,
                })
                .ToListAsync();
        }

        public async Task<ComponentDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(a => a.Id == id)
                .AsQueryable();

            return await query
                .Select(c => new ComponentDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Max = c.Max,
                    
                })
                .FirstOrDefaultAsync();
        }
    }
}