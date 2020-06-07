using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.DAL.Base.EF.Repositories;
using ee.itcollege.kaande.pitsariina.DAL.Base.Mappers;
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
                    PizzaId = cm.PizzaId,
                    Name = cm.Name,
                    PizzaGross = cm.PizzaGross,
                    Changes = cm.Changes,
                    ComponentsGross = cm.ComponentsGross,
                    TotalGross = cm.TotalGross,
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
                    PizzaId = cm.PizzaId,
                    Name = cm.Name,
                    PizzaGross = cm.PizzaGross,
                    Changes = cm.Changes,
                    ComponentsGross = cm.ComponentsGross,
                    TotalGross = cm.TotalGross,
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
                        PaymentMethod = cm.Cart.PaymentMethod,
                        FirstName = cm.Cart.FirstName,
                        LastName = cm.Cart.LastName,
                        Phone = cm.Cart.Phone,
                    },
                    PizzaId = cm.PizzaId,
                    Pizza = cm.Pizza == null
                        ? null
                        : new Pizza()
                        {
                            Id = cm.Pizza.Id,
                            PizzaTemplateId = cm.Pizza.PizzaTemplateId,
                            PizzaTemplate = new PizzaTemplate()
                            {
                                Id = cm.Pizza.PizzaTemplate.Id,
                                CategoryId = cm.Pizza.PizzaTemplate.CategoryId,
                                Category = new Category()
                                {
                                    Id = cm.Pizza.PizzaTemplate.Category.Id,
                                    Name = cm.Pizza.PizzaTemplate.Category.Name,
                                },
                                Name = cm.Pizza.PizzaTemplate.Name,
                                Picture = cm.Pizza.PizzaTemplate.Picture,
                                Modifications = cm.Pizza.PizzaTemplate.Modifications,
                                Extras = cm.Pizza.PizzaTemplate.Extras,
                                Description = cm.Pizza.PizzaTemplate.Description,
                                VarietyState = cm.Pizza.PizzaTemplate.VarietyState,
                            },
                            SizeNumber = cm.Pizza.SizeNumber,
                            SizeName = cm.Pizza.SizeName,
                            Name = cm.Pizza.Name,
                        },
                    Name = cm.Name,
                    PizzaGross = cm.PizzaGross,
                    Changes = cm.Changes,
                    ComponentsGross = cm.ComponentsGross,
                    TotalGross = cm.TotalGross,
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
                        PaymentMethod = cm.Cart.PaymentMethod,
                        FirstName = cm.Cart.FirstName,
                        LastName = cm.Cart.LastName,
                        Phone = cm.Cart.Phone,
                    },
                    PizzaId = cm.PizzaId,
                    Pizza = cm.Pizza == null
                        ? null
                        : new Pizza()
                        {
                            Id = cm.Pizza.Id,
                            PizzaTemplateId = cm.Pizza.PizzaTemplateId,
                            PizzaTemplate = new PizzaTemplate()
                            {
                                Id = cm.Pizza.PizzaTemplate.Id,
                                CategoryId = cm.Pizza.PizzaTemplate.CategoryId,
                                Category = new Category()
                                {
                                    Id = cm.Pizza.PizzaTemplate.Category.Id,
                                    Name = cm.Pizza.PizzaTemplate.Category.Name,
                                },
                                Name = cm.Pizza.PizzaTemplate.Name,
                                Picture = cm.Pizza.PizzaTemplate.Picture,
                                Modifications = cm.Pizza.PizzaTemplate.Modifications,
                                Extras = cm.Pizza.PizzaTemplate.Extras,
                                Description = cm.Pizza.PizzaTemplate.Description,
                                VarietyState = cm.Pizza.PizzaTemplate.VarietyState,
                            },
                            SizeNumber = cm.Pizza.SizeNumber,
                            SizeName = cm.Pizza.SizeName,
                            Name = cm.Pizza.Name,
                        },
                    Name = cm.Name,
                    PizzaGross = cm.PizzaGross,
                    Changes = cm.Changes,
                    ComponentsGross = cm.ComponentsGross,
                    TotalGross = cm.TotalGross,
                })
                .FirstOrDefaultAsync();
        }
    }
}