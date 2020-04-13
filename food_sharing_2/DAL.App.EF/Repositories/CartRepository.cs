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
    public class CartRepository : EFBaseRepository<Cart, AppDbContext>, ICartRepository
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Cart>> AllAsync(Guid? userId = null)
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

            return await query.ToListAsync();
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

            return await query.FirstOrDefaultAsync();
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
            var cartMeal = await FirstOrDefaultAsync(id, userId);
            base.Remove(cartMeal);
        }

        public async Task<IEnumerable<CartDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new CartDTO()
                {
                    Id = c.Id,
                    AppUserId = c.AppUser.Id,    // appUser
                    AsDelivery = c.AsDelivery,
                    UserLocationId = c.UserLocationId,
                    UserLocation = c.UserLocation == null
                        ? null
                        : new UserLocationDTO()
                        {
                            Id = c.UserLocation.Id,
                            AppUserId = c.UserLocation.AppUser.Id,
                            District = c.UserLocation.District,
                            StreetName = c.UserLocation.StreetName,
                            BuildingNumber = c.UserLocation.BuildingNumber,
                            ApartmentNumber = c.UserLocation.ApartmentNumber
                        },
                    RestaurantId = c.RestaurantId,
                    Restaurant = new RestaurantDTO()
                    {
                        Id = c.Restaurant.Id,
                        Name = c.Restaurant.Name,
                        Location = c.Restaurant.Location,
                        Telephone = c.Restaurant.Telephone,
                        OpenTime = c.Restaurant.OpenTime,
                        OpenNotification = c.Restaurant.OpenNotification
                    },
                    Total = c.Total,
                    ReadyBy = c.ReadyBy
                })
                .ToListAsync();
        }

        public async Task<CartDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            CartDTO cartMealDTO = await query
                .Select(c => new CartDTO()
                {
                    Id = c.Id,
                    AppUserId = c.AppUser.Id,    // appUser
                    AsDelivery = c.AsDelivery,
                    UserLocationId = c.UserLocationId,
                    UserLocation = c.UserLocation == null
                        ? null
                        : new UserLocationDTO()
                        {
                            Id = c.UserLocation.Id,
                            AppUserId = c.UserLocation.AppUser.Id,
                            District = c.UserLocation.District,
                            StreetName = c.UserLocation.StreetName,
                            BuildingNumber = c.UserLocation.BuildingNumber,
                            ApartmentNumber = c.UserLocation.ApartmentNumber
                        },
                    RestaurantId = c.RestaurantId,
                    Restaurant = new RestaurantDTO()
                    {
                        Id = c.Restaurant.Id,
                        Name = c.Restaurant.Name,
                        Location = c.Restaurant.Location,
                        Telephone = c.Restaurant.Telephone,
                        OpenTime = c.Restaurant.OpenTime,
                        OpenNotification = c.Restaurant.OpenNotification
                    },
                    Total = c.Total,
                    ReadyBy = c.ReadyBy
                })
                .FirstOrDefaultAsync();
            
            return cartMealDTO;
        }
    }
}