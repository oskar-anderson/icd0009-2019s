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
    public class RestaurantFoodRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.RestaurantFood, RestaurantFood>, 
        IRestaurantFoodRepository
    {
        public RestaurantFoodRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.RestaurantFood, RestaurantFood>())
        {
        }

         /*
        public async Task<IEnumerable<RestaurantFood>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(rf => rf.Restaurant)
                .Include(rf => rf.Meal)
                .Include(rf => rf.Pizza)
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<RestaurantFood> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(rf => rf.Restaurant)
                .Include(rf => rf.Meal)
                .Include(rf => rf.Pizza)
                .AsQueryable();
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(rf => rf.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var restaurantFood = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(restaurantFood.Id);
        }
        
        */
        public virtual async Task<IEnumerable<RestaurantFood>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(rf => new RestaurantFood()
                {
                    Id = rf.Id,
                    PizzaId = rf.PizzaId,
                    Pizza = rf.Pizza == null
                        ? null
                        : new Pizza()
                        {
                            Name = rf.Pizza.Name,
                        },
                    RestaurantId = rf.RestaurantId,
                    Restaurant = new Restaurant()
                    {
                        Name = rf.Restaurant.Name,
                    },
                    Gross = rf.Gross,
                })
                .ToListAsync();
        
        }

        public virtual async Task<RestaurantFood> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(rf => rf.Id == id).AsQueryable();

            return await query
                .Select(rf => new RestaurantFood()
                {
                    Id = rf.Id,
                    PizzaId = rf.PizzaId,
                    Pizza = rf.Pizza == null
                        ? null
                        : new Pizza()
                        {
                            Name = rf.Pizza.Name,
                        },
                    RestaurantId = rf.RestaurantId,
                    Restaurant = new Restaurant()
                    {
                        Name = rf.Restaurant.Name,
                    },
                    Gross = rf.Gross,
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<RestaurantFood>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(rf => new RestaurantFood()
                {
                    Id = rf.Id,
                    PizzaId = rf.PizzaId,
                    Pizza = rf.Pizza == null
                        ? null
                        : new Pizza()
                        {
                            Id = rf.Pizza.Id,
                            PizzaTemplateId = rf.Pizza.PizzaTemplateId,
                            PizzaTemplate = new PizzaTemplate()
                            {
                                Id = rf.Pizza.PizzaTemplate.Id,
                                CategoryId = rf.Pizza.PizzaTemplate.CategoryId,
                                Category = new Category()
                                {
                                    Id = rf.Pizza.PizzaTemplate.Category.Id,
                                    Name = rf.Pizza.PizzaTemplate.Category.Name,
                                },
                                Name = rf.Pizza.PizzaTemplate.Name,
                                Picture = rf.Pizza.PizzaTemplate.Picture,
                                Modifications = rf.Pizza.PizzaTemplate.Modifications,
                                Extras = rf.Pizza.PizzaTemplate.Extras,
                                Description = rf.Pizza.PizzaTemplate.Description,
                                VarietyState = rf.Pizza.PizzaTemplate.VarietyState,
                            },
                            SizeNumber = rf.Pizza.SizeNumber,
                            SizeName = rf.Pizza.SizeName,
                            Name = rf.Pizza.Name,
                        },
                    RestaurantId = rf.RestaurantId,
                    Restaurant = new Restaurant()
                    {
                        Id = rf.Restaurant.Id,
                        Name = rf.Restaurant.Name,
                        Location = rf.Restaurant.Location,
                        Telephone = rf.Restaurant.Telephone,
                        OpenTime = rf.Restaurant.OpenTime,
                        OpenNotification = rf.Restaurant.OpenNotification
                    },
                    Gross = rf.Gross,
                })
                .ToListAsync();
        }

        public virtual async Task<RestaurantFood> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(rf => rf.Id == id).AsQueryable();
            
            return await query
                .Select(rf => new RestaurantFood()
                {
                    Id = rf.Id,
                    PizzaId = rf.PizzaId,
                    Pizza = rf.Pizza == null
                        ? null
                        : new Pizza()
                        {
                            Id = rf.Pizza.Id,
                            PizzaTemplateId = rf.Pizza.PizzaTemplateId,
                            PizzaTemplate = new PizzaTemplate()
                            {
                                Id = rf.Pizza.PizzaTemplate.Id,
                                CategoryId = rf.Pizza.PizzaTemplate.CategoryId,
                                Category = new Category()
                                {
                                    Id = rf.Pizza.PizzaTemplate.Category.Id,
                                    Name = rf.Pizza.PizzaTemplate.Category.Name,
                                },
                                Name = rf.Pizza.PizzaTemplate.Name,
                                Picture = rf.Pizza.PizzaTemplate.Picture,
                                Modifications = rf.Pizza.PizzaTemplate.Modifications,
                                Extras = rf.Pizza.PizzaTemplate.Extras,
                                Description = rf.Pizza.PizzaTemplate.Description,
                                VarietyState = rf.Pizza.PizzaTemplate.VarietyState,
                            },
                            SizeNumber = rf.Pizza.SizeNumber,
                            SizeName = rf.Pizza.SizeName,
                            Name = rf.Pizza.Name,
                        },
                    RestaurantId = rf.RestaurantId,
                    Restaurant = new Restaurant()
                    {
                        Id = rf.Restaurant.Id,
                        Name = rf.Restaurant.Name,
                        Location = rf.Restaurant.Location,
                        Telephone = rf.Restaurant.Telephone,
                        OpenTime = rf.Restaurant.OpenTime,
                        OpenNotification = rf.Restaurant.OpenNotification
                    },
                    Gross = rf.Gross,
                })
                .FirstOrDefaultAsync();
        }
    }
}