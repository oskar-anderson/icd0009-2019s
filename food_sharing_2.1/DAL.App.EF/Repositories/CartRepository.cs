﻿using System;
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
    public class CartRepository : EFBaseRepository<AppDbContext,  Domain.Cart, DAL.App.DTO.Cart>, ICartRepository
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.Cart, DAL.App.DTO.Cart>())
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
            var cartMeal = await FirstOrDefaultAsync(id, userId);
            base.Remove(cartMeal);
        }
        /*
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
        */
    }
}