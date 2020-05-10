using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ComponentRepository :  EFBaseRepository<AppDbContext,  Domain.Component, DAL.App.DTO.Component>, IComponentRepository
    {
        public ComponentRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.Component, DAL.App.DTO.Component>())
        {
        }

        public async Task<IEnumerable<Component>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Component> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(cm => cm.Id == id)
                .AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
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

        /*
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
        */
    }
}