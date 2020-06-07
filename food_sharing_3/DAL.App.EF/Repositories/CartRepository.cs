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
    public class CartRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Cart, DAL.App.DTO.Cart>, 
        ICartRepository
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.Cart, Cart>())
        {
        }
        
        /*
        
        public async Task<IEnumerable<Cart>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(c => c.Restaurant)
                .Include(c => c.AppUser)
                .Include(c => c.UserLocation)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(c => c.AppUser.Id == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Cart> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(c => c.Restaurant)
                .Include(c => c.AppUser)
                .Include(c => c.UserLocation)
                .Where(c => c.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(c => c.AppUser.Id == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(c => c.Id == id);
            }

            return await RepoDbSet.AnyAsync(c => c.AppUser.Id == userId);

        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var cart = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(cart.Id);
        }

        */
        public virtual async Task<IEnumerable<DAL.App.DTO.Cart>> GetAllForViewAsync(Guid userId)
        {
            var query = RepoDbSet.Where(c => c.AppUserId == userId).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Cart()
                {
                    Id = c.Id,
                    AppUserId = c.AppUser.Id,    // appUser
                    RestaurantId = c.RestaurantId,
                    Restaurant = new Restaurant()
                    {
                        Name = c.Restaurant.Name,
                    },
                    AsDelivery = c.AsDelivery,
                    UserLocationId = c.UserLocationId,
                    UserLocation = c.UserLocation == null
                        ? null
                        : new UserLocation()
                        {
                            District = c.UserLocation.District,
                            StreetName = c.UserLocation.StreetName,
                            BuildingNumber = c.UserLocation.BuildingNumber,
                            ApartmentNumber = c.UserLocation.ApartmentNumber,

                        },
                    PaymentMethod = c.PaymentMethod,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Phone = c.Phone,
                })
                .ToListAsync();
        }

        public virtual async Task<Cart> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Cart()
                {
                    Id = c.Id,
                    AppUserId = c.AppUser.Id,    // appUser
                    RestaurantId = c.RestaurantId,
                    Restaurant = new Restaurant()
                    {
                        Name = c.Restaurant.Name,
                    },
                    AsDelivery = c.AsDelivery,
                    UserLocationId = c.UserLocationId,
                    UserLocation = c.UserLocation == null
                        ? null
                        : new UserLocation()
                        {
                            District = c.UserLocation.District,
                            StreetName = c.UserLocation.StreetName,
                            BuildingNumber = c.UserLocation.BuildingNumber,
                            ApartmentNumber = c.UserLocation.ApartmentNumber
                        },
                    PaymentMethod = c.PaymentMethod,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Phone = c.Phone,
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<Cart>> GetAllForApiAsync(Guid userId)
        {
            var query = RepoDbSet.Where(c => c.AppUserId == userId).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Cart()
                {
                    Id = c.Id,
                    AppUserId = c.AppUser.Id,    // appUser
                    RestaurantId = c.RestaurantId,
                    Restaurant = new Restaurant()
                    {
                        Id = c.Restaurant.Id,
                        Name = c.Restaurant.Name,
                        Location = c.Restaurant.Location,
                        Telephone = c.Restaurant.Telephone,
                        OpenTime = c.Restaurant.OpenTime,
                        OpenNotification = c.Restaurant.OpenNotification
                    },
                    AsDelivery = c.AsDelivery,
                    UserLocationId = c.UserLocationId,
                    UserLocation = c.UserLocation == null
                        ? null
                        : new UserLocation()
                        {
                            Id = c.UserLocation.Id,
                            AppUserId = c.UserLocation.AppUser.Id,
                            District = c.UserLocation.District,
                            StreetName = c.UserLocation.StreetName,
                            BuildingNumber = c.UserLocation.BuildingNumber,
                            ApartmentNumber = c.UserLocation.ApartmentNumber
                        },
                    PaymentMethod = c.PaymentMethod,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Phone = c.Phone,
                })
                .ToListAsync();
        }

        public virtual async Task<Cart> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();
            
            return await query
                .Select(c => new DAL.App.DTO.Cart()
                {
                    Id = c.Id,
                    AppUserId = c.AppUser.Id,    // appUser
                    RestaurantId = c.RestaurantId,
                    Restaurant = new Restaurant()
                    {
                        Id = c.Restaurant.Id,
                        Name = c.Restaurant.Name,
                        Location = c.Restaurant.Location,
                        Telephone = c.Restaurant.Telephone,
                        OpenTime = c.Restaurant.OpenTime,
                        OpenNotification = c.Restaurant.OpenNotification
                    },
                    AsDelivery = c.AsDelivery,
                    UserLocationId = c.UserLocationId,
                    UserLocation = c.UserLocation == null
                        ? null
                        : new UserLocation()
                        {
                            Id = c.UserLocation.Id,
                            AppUserId = c.UserLocation.AppUser.Id,
                            District = c.UserLocation.District,
                            StreetName = c.UserLocation.StreetName,
                            BuildingNumber = c.UserLocation.BuildingNumber,
                            ApartmentNumber = c.UserLocation.ApartmentNumber
                        },
                    PaymentMethod = c.PaymentMethod,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Phone = c.Phone,
                })
                .FirstOrDefaultAsync();
        }
    }
}