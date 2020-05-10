using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Domain.Base.EF.Repositories;
using Domain.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace Domain.Base.App.EF.Repositories
{
    public class ComponentPriceRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.ComponentPrice, DTO.ComponentPrice>, 
        IComponentPriceRepository
    {
        public ComponentPriceRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.ComponentPrice, DTO.ComponentPrice>())
        {
        }

        public async Task<IEnumerable<DTO.ComponentPrice>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(cp => cp.Component)
                .Include(cp => cp.Restaurant)
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.ComponentPrice> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(cp => cp.Component)
                .Include(cp => cp.Restaurant)
                .Where(cp => cp.Id == id)
                .AsQueryable();
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(cp => cp.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var componentPrice = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(componentPrice.Id);
        }

        /*
        public async Task<IEnumerable<ComponentPriceDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(cp => new ComponentPriceDTO()
                {
                    Id = cp.Id,
                    
                    ComponentId = cp.ComponentId,
                    Component = new ComponentDTO()
                    {
                        Id = cp.Component.Id,
                        Name = cp.Component.Name,
                        Max = cp.Component.Max
                    },
                    RestaurantId = cp.RestaurantId,
                    Restaurant = new RestaurantDTO()
                    {
                        Id = cp.Restaurant.Id,
                        Name = cp.Restaurant.Name,
                        Location = cp.Restaurant.Location,
                        Telephone = cp.Restaurant.Telephone,
                        OpenTime = cp.Restaurant.OpenTime,
                        OpenNotification = cp.Restaurant.OpenNotification
                    },
                    Tax = cp.Tax,
                    Gross = cp.Gross,
                    Since = cp.Since,
                    Until = cp.Until,
                })
                .ToListAsync();
        }

        public async Task<ComponentPriceDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            ComponentPriceDTO componentPrice = await query
                .Select(cp => new ComponentPriceDTO()
                {
                    Id = cp.Id,
                    
                    ComponentId = cp.ComponentId,
                    Component = new ComponentDTO()
                    {
                        Id = cp.Component.Id,
                        Name = cp.Component.Name,
                        Max = cp.Component.Max
                    },
                    RestaurantId = cp.RestaurantId,
                    Restaurant = new RestaurantDTO()
                    {
                        Id = cp.Restaurant.Id,
                        Name = cp.Restaurant.Name,
                        Location = cp.Restaurant.Location,
                        Telephone = cp.Restaurant.Telephone,
                        OpenTime = cp.Restaurant.OpenTime,
                        OpenNotification = cp.Restaurant.OpenNotification
                    },
                    Tax = cp.Tax,
                    Gross = cp.Gross,
                    Since = cp.Since,
                    Until = cp.Until,
                })
                .FirstOrDefaultAsync();
            
            return componentPrice;
        }
        */
    }
}