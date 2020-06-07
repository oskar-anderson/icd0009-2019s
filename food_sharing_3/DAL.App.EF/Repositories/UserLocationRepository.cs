using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Domain.App.Identity;
using ee.itcollege.kaande.pitsariina.DAL.Base.EF.Repositories;
using ee.itcollege.kaande.pitsariina.DAL.Base.Mappers;
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

        /*
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
        
        */
        public virtual async Task<IEnumerable<UserLocation>> GetAllForViewAsync(Guid userId)
        {
            var query = RepoDbSet.Where(c => c.AppUserId == userId).AsQueryable();
            
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

        public virtual async Task<UserLocation> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(ul => ul.Id == id).AsQueryable();

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
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<UserLocation>> GetAllForApiAsync(Guid userId)
        {
            var query = RepoDbSet.Where(c => c.AppUserId == userId).AsQueryable();
            
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

        public virtual async Task<UserLocation> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(ul => ul.Id == id).AsQueryable();

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
                .FirstOrDefaultAsync();
        }
    }
}