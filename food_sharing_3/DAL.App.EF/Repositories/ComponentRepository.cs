using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Domain.App.Identity;
using Domain.Base.App.EF;
using ee.itcollege.kaande.pitsariina.DAL.Base.EF.Repositories;
using ee.itcollege.kaande.pitsariina.DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ComponentRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Component, Component>, 
        IComponentRepository
    {
        public ComponentRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.Component, Component>())
        {
        }

        /*
        public async Task<IEnumerable<Component>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
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
            await base.RemoveAsync(component.Id);
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
                    
                })
                .FirstOrDefaultAsync();
        }
        */
    }
}