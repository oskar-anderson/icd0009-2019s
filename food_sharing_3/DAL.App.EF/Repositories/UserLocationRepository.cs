using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserLocationRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.UserLocation, UserLocation>, 
        IUserLocationRepository
    {
        public UserLocationRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.UserLocation, UserLocation>())
        {
        }

        public async Task<IEnumerable<UserLocation>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(ul => ul.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(ul => ul.AppUserId == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<UserLocation> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(ul => ul.AppUser)
                .Where(ul => ul.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(ul => ul.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(ul => ul.Id == id);
            }

            return await RepoDbSet.AnyAsync(ul => ul.AppUserId == userId);

        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var userLocation = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(userLocation.Id);
        }

        /*
        public async Task<IEnumerable<UserLocationDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(ul => new UserLocationDTO()
                {
                    Id = ul.Id,
                    AppUserId = ul.AppUserId,
                    District = ul.District,
                    StreetName = ul.StreetName,
                    BuildingNumber = ul.BuildingNumber,
                    ApartmentNumber = ul.ApartmentNumber,
                })
                .ToListAsync();
        }

        public async Task<UserLocationDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            UserLocationDTO userLocationDTO = await query
                .Select(ul => new UserLocationDTO()
                {
                    Id = ul.Id,
                    AppUserId = ul.AppUserId,
                    District = ul.District,
                    StreetName = ul.StreetName,
                    BuildingNumber = ul.BuildingNumber,
                    ApartmentNumber = ul.ApartmentNumber,
                })
                .FirstOrDefaultAsync();
            
            return userLocationDTO;
        }
        */
        public virtual async Task<IEnumerable<UserLocation>> GetAllForViewAsync()
        {
            var query = RepoDbSet.AsQueryable();

            return await query
                .Select(ul => new UserLocation()
                {
                    Id = ul.Id,
                    AppUserId = ul.AppUserId,
                    District = ul.District,
                    StreetName = ul.StreetName,
                    BuildingNumber = ul.BuildingNumber,
                    ApartmentNumber = ul.ApartmentNumber,
                    
                })
                .ToListAsync();
        }
    }
}