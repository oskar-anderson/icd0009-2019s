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
    public class RestaurantRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Restaurant, DTO.Restaurant>, 
        IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.Restaurant, DTO.Restaurant>())
        {
        }

        public async Task<IEnumerable<DTO.Restaurant>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .AsQueryable();

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
            
            // return await base.AllAsync();
        }

        public async Task<DTO.Restaurant> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(cm => cm.Id == id)
                .AsQueryable();

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(r => r.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var restaurant = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(restaurant.Id);
        }
        /*
        public async Task<IEnumerable<RestaurantDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            return await query
                .Select(r => new RestaurantDTO()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Location = r.Location,
                    Telephone = r.Telephone,
                    OpenTime = r.OpenTime,
                    OpenNotification = r.OpenNotification
                })
                .ToListAsync();
        }

        public async Task<RestaurantDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(r => r.Id == id).AsQueryable();
            
            var restaurantDTO = await query.Select(r => new RestaurantDTO()
            {
                Id = r.Id,
                Name = r.Name,
                Location = r.Location,
                Telephone = r.Telephone,
                OpenTime = r.OpenTime,
                OpenNotification = r.OpenNotification
            }).FirstOrDefaultAsync();

            return restaurantDTO;
        }
        */

    }
}