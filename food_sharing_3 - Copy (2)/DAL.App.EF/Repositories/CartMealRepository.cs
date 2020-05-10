using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CartMealRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.CartMeal, DAL.App.DTO.CartMeal>, 
        ICartMealRepository
    {
        public CartMealRepository(AppDbContext repoDbContext) : base(repoDbContext, 
            new BaseMapper<Domain.App.CartMeal, CartMeal>())
        {
        }
        
        // methods go here
        
        public async Task<IEnumerable<CartMeal>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(cm => cm.PizzaFinal)
                .Include(cm => cm.Meal)
                .Include(cm => cm.Cart)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(cd => cd.Cart.AppUser.Id == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<CartMeal> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(cm => cm.PizzaFinal)
                .Include(cm => cm.Meal)
                .Include(cm => cm.Cart)
                .Where(cm => cm.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(cm => cm.Cart.AppUser.Id == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(cm => cm.Id == id);
            }

            return await RepoDbSet.AnyAsync(cm => cm.Cart.AppUser.Id == userId);

        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(cm => cm.Id == id).AsNoTracking().AsQueryable();

            
            var cartMeal = await query.FirstOrDefaultAsync();
            await base.RemoveAsync(cartMeal.Id);
        }
        /*
        public async Task<IEnumerable<CartMealDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(cm => new CartMealDTO()
                {
                    Id = cm.Id,
                    MealId = cm.MealId,
                    Meal = cm.Meal == null
                        ? null
                        : new MealDTO()
                        {
                            Id = cm.Meal.Id,
                            CategoryId = cm.Meal.CategoryId,
                            Category = new CategoryDTO()
                            {
                                Id = cm.Meal.Category.Id,
                                Name = cm.Meal.Category.Name
                            },
                            Name = cm.Meal.Name,
                            Picture = cm.Meal.Picture,
                            Description = cm.Meal.Description,
                        },
                    PizzaFinalId = cm.PizzaFinalId,
                    PizzaFinal = cm.PizzaFinal == null
                        ? null
                        : new PizzaFinalDTO()
                        {
                            Id = cm.PizzaFinal.Id,
                            PizzaId = cm.PizzaFinal.Pizza.Id,
                            Pizza = new PizzaDTO()
                            {
                                Id = cm.PizzaFinal.Pizza.Id,
                                PizzaTemplateId = cm.PizzaFinal.Pizza.PizzaTemplateId,
                                PizzaTemplate = new PizzaTemplateDTO()
                                {
                                    Id = cm.PizzaFinal.Pizza.PizzaTemplate.Id,
                                    CategoryId = cm.PizzaFinal.Pizza.PizzaTemplate.CategoryId,
                                    Category = new CategoryDTO()
                                    {
                                        Id = cm.PizzaFinal.Pizza.PizzaTemplate.Category.Id,
                                        Name = cm.PizzaFinal.Pizza.PizzaTemplate.Category.Name,
                                    },
                                    Name = cm.PizzaFinal.Pizza.PizzaTemplate.Name,
                                    Picture = cm.PizzaFinal.Pizza.PizzaTemplate.Picture,
                                    Modifications = cm.PizzaFinal.Pizza.PizzaTemplate.Modifications,
                                    Extras = cm.PizzaFinal.Pizza.PizzaTemplate.Extras,
                                    Description = cm.PizzaFinal.Pizza.PizzaTemplate.Description,
                                },
                                SizeId = cm.PizzaFinal.Pizza.SizeId,
                                Size = new SizeDTO()
                                {
                                    Id = cm.PizzaFinal.Pizza.Size.Id,
                                    Name = cm.PizzaFinal.Pizza.Size.Name
                                },
                                Name = cm.PizzaFinal.Pizza.Name,
                            },
                            Changes = cm.PizzaFinal.Changes,
                            Tax = cm.PizzaFinal.Tax,
                            Gross = cm.PizzaFinal.Gross
                        },
                    CartId = cm.Cart.Id,
                    Cart = new CartDTO()
                    {
                        Id = cm.Cart.Id,
                        AppUserId = cm.Cart.AppUser.Id,    // appUser
                        AsDelivery = cm.Cart.AsDelivery,
                        UserLocationId = cm.Cart.UserLocationId,
                        UserLocation = cm.Cart.UserLocation == null
                            ? null
                            : new UserLocationDTO()
                        {
                            Id = cm.Cart.UserLocation.Id,
                            AppUserId = cm.Cart.UserLocation.AppUser.Id,
                            District = cm.Cart.UserLocation.District,
                            StreetName = cm.Cart.UserLocation.StreetName,
                            BuildingNumber = cm.Cart.UserLocation.BuildingNumber,
                            ApartmentNumber = cm.Cart.UserLocation.ApartmentNumber
                        },
                        RestaurantId = cm.Cart.RestaurantId,
                        Restaurant = new RestaurantDTO()
                        {
                            Id = cm.Cart.Restaurant.Id,
                            Name = cm.Cart.Restaurant.Name,
                            Location = cm.Cart.Restaurant.Location,
                            Telephone = cm.Cart.Restaurant.Telephone,
                            OpenTime = cm.Cart.Restaurant.OpenTime,
                            OpenNotification = cm.Cart.Restaurant.OpenNotification
                        },
                        Total = cm.Cart.Total,
                        ReadyBy = cm.Cart.ReadyBy
                    }
                })
                .ToListAsync();
        }

        public async Task<CartMealDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            CartMealDTO cartMealDTO = await query
                .Select(cm => new CartMealDTO()
                {
                    Id = cm.Id,
                    MealId = cm.MealId,
                    Meal = cm.Meal == null
                        ? null
                        : new MealDTO()
                        {
                            Id = cm.Meal.Id,
                            CategoryId = cm.Meal.CategoryId,
                            Category = new CategoryDTO()
                            {
                                Id = cm.Meal.Category.Id,
                                Name = cm.Meal.Category.Name
                            },
                            Name = cm.Meal.Name,
                            Picture = cm.Meal.Picture,
                            Description = cm.Meal.Description,
                        },
                    PizzaFinalId = cm.PizzaFinalId,
                    PizzaFinal = cm.PizzaFinal == null
                        ? null
                        : new PizzaFinalDTO()
                        {
                            Id = cm.PizzaFinal.Id,
                            PizzaId = cm.PizzaFinal.Pizza.Id,
                            Pizza = new PizzaDTO()
                            {
                                Id = cm.PizzaFinal.Pizza.Id,
                                PizzaTemplateId = cm.PizzaFinal.Pizza.PizzaTemplateId,
                                PizzaTemplate = new PizzaTemplateDTO()
                                {
                                    Id = cm.PizzaFinal.Pizza.PizzaTemplate.Id,
                                    CategoryId = cm.PizzaFinal.Pizza.PizzaTemplate.CategoryId,
                                    Category = new CategoryDTO()
                                    {
                                        Id = cm.PizzaFinal.Pizza.PizzaTemplate.Category.Id,
                                        Name = cm.PizzaFinal.Pizza.PizzaTemplate.Category.Name,
                                    },
                                    Name = cm.PizzaFinal.Pizza.PizzaTemplate.Name,
                                    Picture = cm.PizzaFinal.Pizza.PizzaTemplate.Picture,
                                    Modifications = cm.PizzaFinal.Pizza.PizzaTemplate.Modifications,
                                    Extras = cm.PizzaFinal.Pizza.PizzaTemplate.Extras,
                                    Description = cm.PizzaFinal.Pizza.PizzaTemplate.Description,
                                },
                                SizeId = cm.PizzaFinal.Pizza.SizeId,
                                Size = new SizeDTO()
                                {
                                    Id = cm.PizzaFinal.Pizza.Size.Id,
                                    Name = cm.PizzaFinal.Pizza.Size.Name
                                },
                                Name = cm.PizzaFinal.Pizza.Name,
                            },
                            Changes = cm.PizzaFinal.Changes,
                            Tax = cm.PizzaFinal.Tax,
                            Gross = cm.PizzaFinal.Gross
                        },
                    CartId = cm.Cart.Id,
                    Cart = new CartDTO()
                    {
                        Id = cm.Cart.Id,
                        AppUserId = cm.Cart.AppUser.Id,    // appUser
                        AsDelivery = cm.Cart.AsDelivery,
                        UserLocationId = cm.Cart.UserLocationId,
                        UserLocation = cm.Cart.UserLocation == null
                            ? null
                            : new UserLocationDTO()
                        {
                            Id = cm.Cart.UserLocation.Id,
                            AppUserId = cm.Cart.UserLocation.AppUser.Id,
                            District = cm.Cart.UserLocation.District,
                            StreetName = cm.Cart.UserLocation.StreetName,
                            BuildingNumber = cm.Cart.UserLocation.BuildingNumber,
                            ApartmentNumber = cm.Cart.UserLocation.ApartmentNumber
                        },
                        RestaurantId = cm.Cart.RestaurantId,
                        Restaurant = new RestaurantDTO()
                        {
                            Id = cm.Cart.Restaurant.Id,
                            Name = cm.Cart.Restaurant.Name,
                            Location = cm.Cart.Restaurant.Location,
                            Telephone = cm.Cart.Restaurant.Telephone,
                            OpenTime = cm.Cart.Restaurant.OpenTime,
                            OpenNotification = cm.Cart.Restaurant.OpenNotification
                        },
                        Total = cm.Cart.Total,
                        ReadyBy = cm.Cart.ReadyBy
                    }
                })
                .FirstOrDefaultAsync();
            
            return cartMealDTO;
        }
        */

    }
}