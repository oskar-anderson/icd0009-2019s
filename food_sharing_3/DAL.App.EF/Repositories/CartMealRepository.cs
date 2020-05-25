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
        /*
        public async Task<IEnumerable<CartMeal>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(cm => cm.PizzaUser)
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
                .Include(cm => cm.PizzaUser)
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
        
        */

        public virtual async Task<IEnumerable<CartMeal>> GetAllForViewAsync()
        {
            var cartMeal =  await RepoDbSet
                .Select(cm => new CartMeal()
                {
                    Id = cm.Id,
                    CartId = cm.Cart.Id,
                    MealId = cm.MealId,
                    PizzaUserId = cm.PizzaUserId,
                    Name = cm.Name,
                    Gross = cm.Gross,
                })
                .ToListAsync();
            return cartMeal;
        }

        public virtual async Task<CartMeal> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(cm => cm.Id == id).AsQueryable();
            
            var cartMeal = await query
                .Select(cm => new CartMeal()
                {
                    Id = cm.Id,
                    CartId = cm.Cart.Id,
                    MealId = cm.MealId,
                    PizzaUserId = cm.PizzaUserId,
                    Name = cm.Name,
                    Gross = cm.Gross,
                })
                .FirstOrDefaultAsync();
            return cartMeal;
        }

        public virtual async Task<IEnumerable<CartMeal>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(cm => new CartMeal()
                {
                    Id = cm.Id,
                    CartId = cm.Cart.Id,
                    Cart = new Cart()
                    {
                        Id = cm.Cart.Id,
                        State = cm.Cart.State,
                        AppUserId = cm.Cart.AppUser.Id,    // appUser
                        RestaurantId = cm.Cart.RestaurantId,
                        Restaurant = new Restaurant()
                        {
                            Id = cm.Cart.Restaurant.Id,
                            Name = cm.Cart.Restaurant.Name,
                            Location = cm.Cart.Restaurant.Location,
                            Telephone = cm.Cart.Restaurant.Telephone,
                            OpenTime = cm.Cart.Restaurant.OpenTime,
                            OpenNotification = cm.Cart.Restaurant.OpenNotification
                        },
                        AsDelivery = cm.Cart.AsDelivery,
                        UserLocationId = cm.Cart.UserLocationId,
                        UserLocation = cm.Cart.UserLocation == null
                            ? null
                            : new UserLocation()
                            {
                                Id = cm.Cart.UserLocation.Id,
                                AppUserId = cm.Cart.UserLocation.AppUser.Id,
                                District = cm.Cart.UserLocation.District,
                                StreetName = cm.Cart.UserLocation.StreetName,
                                BuildingNumber = cm.Cart.UserLocation.BuildingNumber,
                                ApartmentNumber = cm.Cart.UserLocation.ApartmentNumber
                            },
                        Gross = cm.Cart.Gross,
                        PaymentMethod = cm.Cart.PaymentMethod,
                        FirstName = cm.Cart.FirstName,
                        LastName = cm.Cart.LastName,
                        Phone = cm.Cart.Phone,
                        ReadyBy = cm.Cart.ReadyBy
                    },
                    MealId = cm.MealId,
                    Meal = cm.Meal == null
                        ? null
                        : new Meal()
                        {
                            Id = cm.Meal.Id,
                            CategoryId = cm.Meal.CategoryId,
                            Category = new Category()
                            {
                                Id = cm.Meal.Category.Id,
                                Name = cm.Meal.Category.Name,
                                ForMeal = cm.Meal.Category.ForMeal,
                                ForPizzaTemplate = cm.Meal.Category.ForPizzaTemplate,
                            },
                            Name = cm.Meal.Name,
                            Picture = cm.Meal.Picture,
                            Description = cm.Meal.Description,
                        },
                    PizzaUserId = cm.PizzaUserId,
                    PizzaUser = cm.PizzaUser == null
                        ? null
                        : new PizzaUser()
                        {
                            Id = cm.PizzaUser.Id,
                            PizzaId = cm.PizzaUser.Pizza.Id,
                            AppUserId = cm.PizzaUser.AppUserId,
                            Pizza = new Pizza()
                            {
                                Id = cm.PizzaUser.Pizza.Id,
                                PizzaTemplateId = cm.PizzaUser.Pizza.PizzaTemplateId,
                                PizzaTemplate = new PizzaTemplate()
                                {
                                    Id = cm.PizzaUser.Pizza.PizzaTemplate.Id,
                                    CategoryId = cm.PizzaUser.Pizza.PizzaTemplate.CategoryId,
                                    Category = new Category()
                                    {
                                        Id = cm.Meal.Category.Id,
                                        Name = cm.Meal.Category.Name,
                                        ForMeal = cm.Meal.Category.ForMeal,
                                        ForPizzaTemplate = cm.Meal.Category.ForPizzaTemplate,
                                    },
                                    Name = cm.PizzaUser.Pizza.PizzaTemplate.Name,
                                    Picture = cm.PizzaUser.Pizza.PizzaTemplate.Picture,
                                    Modifications = cm.PizzaUser.Pizza.PizzaTemplate.Modifications,
                                    Extras = cm.PizzaUser.Pizza.PizzaTemplate.Extras,
                                    Description = cm.PizzaUser.Pizza.PizzaTemplate.Description,
                                },
                                SizeNumber = cm.PizzaUser.Pizza.SizeNumber,
                                SizeName = cm.PizzaUser.Pizza.SizeName,
                                Name = cm.PizzaUser.Pizza.Name,
                            },
                            Changes = cm.PizzaUser.Changes,
                        },
                })
                .ToListAsync();
        }

        public virtual async Task<CartMeal> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(cm => cm.Id == id).AsQueryable();
            
            return await query
                .Select(cm => new CartMeal()
                {
                    Id = cm.Id,
                    CartId = cm.Cart.Id,
                    Cart = new Cart()
                    {
                        Id = cm.Cart.Id,
                        State = cm.Cart.State,
                        AppUserId = cm.Cart.AppUser.Id,    // appUser
                        RestaurantId = cm.Cart.RestaurantId,
                        Restaurant = new Restaurant()
                        {
                            Id = cm.Cart.Restaurant.Id,
                            Name = cm.Cart.Restaurant.Name,
                            Location = cm.Cart.Restaurant.Location,
                            Telephone = cm.Cart.Restaurant.Telephone,
                            OpenTime = cm.Cart.Restaurant.OpenTime,
                            OpenNotification = cm.Cart.Restaurant.OpenNotification
                        },
                        AsDelivery = cm.Cart.AsDelivery,
                        UserLocationId = cm.Cart.UserLocationId,
                        UserLocation = cm.Cart.UserLocation == null
                            ? null
                            : new UserLocation()
                            {
                                Id = cm.Cart.UserLocation.Id,
                                AppUserId = cm.Cart.UserLocation.AppUser.Id,
                                District = cm.Cart.UserLocation.District,
                                StreetName = cm.Cart.UserLocation.StreetName,
                                BuildingNumber = cm.Cart.UserLocation.BuildingNumber,
                                ApartmentNumber = cm.Cart.UserLocation.ApartmentNumber
                            },
                        Gross = cm.Cart.Gross,
                        PaymentMethod = cm.Cart.PaymentMethod,
                        FirstName = cm.Cart.FirstName,
                        LastName = cm.Cart.LastName,
                        Phone = cm.Cart.Phone,
                        ReadyBy = cm.Cart.ReadyBy
                    },
                    MealId = cm.MealId,
                    Meal = cm.Meal == null
                        ? null
                        : new Meal()
                        {
                            Id = cm.Meal.Id,
                            CategoryId = cm.Meal.CategoryId,
                            Category = new Category()
                            {
                                Id = cm.Meal.Category.Id,
                                Name = cm.Meal.Category.Name,
                                ForMeal = cm.Meal.Category.ForMeal,
                                ForPizzaTemplate = cm.Meal.Category.ForPizzaTemplate,
                            },
                            Name = cm.Meal.Name,
                            Picture = cm.Meal.Picture,
                            Description = cm.Meal.Description,
                        },
                    PizzaUserId = cm.PizzaUserId,
                    PizzaUser = cm.PizzaUser == null
                        ? null
                        : new PizzaUser()
                        {
                            Id = cm.PizzaUser.Id,
                            PizzaId = cm.PizzaUser.Pizza.Id,
                            AppUserId = cm.PizzaUser.AppUserId,
                            Pizza = new Pizza()
                            {
                                Id = cm.PizzaUser.Pizza.Id,
                                PizzaTemplateId = cm.PizzaUser.Pizza.PizzaTemplateId,
                                PizzaTemplate = new PizzaTemplate()
                                {
                                    Id = cm.PizzaUser.Pizza.PizzaTemplate.Id,
                                    CategoryId = cm.PizzaUser.Pizza.PizzaTemplate.CategoryId,
                                    Category = new Category()
                                    {
                                        Id = cm.Meal.Category.Id,
                                        Name = cm.Meal.Category.Name,
                                        ForMeal = cm.Meal.Category.ForMeal,
                                        ForPizzaTemplate = cm.Meal.Category.ForPizzaTemplate,
                                    },
                                    Name = cm.PizzaUser.Pizza.PizzaTemplate.Name,
                                    Picture = cm.PizzaUser.Pizza.PizzaTemplate.Picture,
                                    Modifications = cm.PizzaUser.Pizza.PizzaTemplate.Modifications,
                                    Extras = cm.PizzaUser.Pizza.PizzaTemplate.Extras,
                                    Description = cm.PizzaUser.Pizza.PizzaTemplate.Description,
                                },
                                SizeNumber = cm.PizzaUser.Pizza.SizeNumber,
                                SizeName = cm.PizzaUser.Pizza.SizeName,
                                Name = cm.PizzaUser.Pizza.Name,
                            },
                            Changes = cm.PizzaUser.Changes,
                        },
                })
                .FirstOrDefaultAsync();
        }
    }
}