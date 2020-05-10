﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Domain.Base.EF.Repositories;
using Domain.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Base.App.EF.Repositories
{
    public class RestaurantFoodRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.RestaurantFood, DTO.RestaurantFood>, 
        IRestaurantFoodRepository
    {
        public RestaurantFoodRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.RestaurantFood, DTO.RestaurantFood>())
        {
        }

        public async Task<IEnumerable<DTO.RestaurantFood>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(rf => rf.Restaurant)
                .Include(rf => rf.Meal)
                .Include(rf => rf.Pizza)
                .AsQueryable();
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.RestaurantFood> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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

        /*
        public async Task<IEnumerable<RestaurantFoodDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();
            return await query
                .Select(rf => new RestaurantFoodDTO()
                {
                    Id = rf.Id,
                    MealId = rf.MealId,
                    Meal = rf.Meal == null
                        ? null
                        : new MealDTO()
                        {
                            Id = rf.Meal.Id,
                            CategoryId = rf.Meal.CategoryId,
                            Category = new CategoryDTO()
                            {
                                Id = rf.Meal.Category.Id,
                                Name = rf.Meal.Category.Name
                            },
                            Name = rf.Meal.Name,
                            Picture = rf.Meal.Picture,
                            Description = rf.Meal.Description,
                        },
                    PizzaId = rf.PizzaId,
                    Pizza = rf.Pizza == null
                        ? null
                        : new PizzaDTO()
                        {
                            Id = rf.Pizza.Id,
                            PizzaTemplateId = rf.Pizza.PizzaTemplateId,
                            PizzaTemplate = new PizzaTemplateDTO()
                            {
                                Id = rf.Pizza.PizzaTemplate.Id,
                                CategoryId = rf.Pizza.PizzaTemplate.CategoryId,
                                Category = new CategoryDTO()
                                {
                                    Id = rf.Pizza.PizzaTemplate.Category.Id,
                                    Name = rf.Pizza.PizzaTemplate.Category.Name,
                                },
                                Name = rf.Pizza.PizzaTemplate.Name,
                                Picture = rf.Pizza.PizzaTemplate.Picture,
                                Modifications = rf.Pizza.PizzaTemplate.Modifications,
                                Extras = rf.Pizza.PizzaTemplate.Extras,
                                Description = rf.Pizza.PizzaTemplate.Description,
                            },
                            SizeId = rf.Pizza.SizeId,
                            Size = new SizeDTO()
                            {
                                Id = rf.Pizza.Size.Id,
                                Name = rf.Pizza.Size.Name
                            },
                            Name = rf.Pizza.Name,
                        },
                    RestaurantId = rf.RestaurantId,
                    Restaurant = new RestaurantDTO()
                    {
                        Id = rf.Restaurant.Id,
                        Name = rf.Restaurant.Name,
                        Location = rf.Restaurant.Location,
                        Telephone = rf.Restaurant.Telephone,
                        OpenTime = rf.Restaurant.OpenTime,
                        OpenNotification = rf.Restaurant.OpenNotification
                    },
                    Name = rf.Name,
                    Tax = rf.Tax,
                    Gross = rf.Gross,
                    Since = rf.Since,
                    Until = rf.Until
                })
                .ToListAsync();
        }

        public async Task<RestaurantFoodDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            RestaurantFoodDTO restaurantFoodDTO = await query
                .Select(rf => new RestaurantFoodDTO()
                {
                    Id = rf.Id,
                    MealId = rf.MealId,
                    Meal = rf.Meal == null
                        ? null
                        : new MealDTO()
                        {
                            Id = rf.Meal.Id,
                            CategoryId = rf.Meal.CategoryId,
                            Category = new CategoryDTO()
                            {
                                Id = rf.Meal.Category.Id,
                                Name = rf.Meal.Category.Name
                            },
                            Name = rf.Meal.Name,
                            Picture = rf.Meal.Picture,
                            Description = rf.Meal.Description,
                        },
                    PizzaId = rf.PizzaId,
                    Pizza = rf.Pizza == null
                        ? null
                        : new PizzaDTO()
                        {
                            Id = rf.Pizza.Id,
                            PizzaTemplateId = rf.Pizza.PizzaTemplateId,
                            PizzaTemplate = new PizzaTemplateDTO()
                            {
                                Id = rf.Pizza.PizzaTemplate.Id,
                                CategoryId = rf.Pizza.PizzaTemplate.CategoryId,
                                Category = new CategoryDTO()
                                {
                                    Id = rf.Pizza.PizzaTemplate.Category.Id,
                                    Name = rf.Pizza.PizzaTemplate.Category.Name,
                                },
                                Name = rf.Pizza.PizzaTemplate.Name,
                                Picture = rf.Pizza.PizzaTemplate.Picture,
                                Modifications = rf.Pizza.PizzaTemplate.Modifications,
                                Extras = rf.Pizza.PizzaTemplate.Extras,
                                Description = rf.Pizza.PizzaTemplate.Description,
                            },
                            SizeId = rf.Pizza.SizeId,
                            Size = new SizeDTO()
                            {
                                Id = rf.Pizza.Size.Id,
                                Name = rf.Pizza.Size.Name
                            },
                            Name = rf.Pizza.Name,
                        },
                    RestaurantId = rf.RestaurantId,
                    Restaurant = new RestaurantDTO()
                    {
                        Id = rf.Restaurant.Id,
                        Name = rf.Restaurant.Name,
                        Location = rf.Restaurant.Location,
                        Telephone = rf.Restaurant.Telephone,
                        OpenTime = rf.Restaurant.OpenTime,
                        OpenNotification = rf.Restaurant.OpenNotification
                    },
                    Name = rf.Name,
                    Tax = rf.Tax,
                    Gross = rf.Gross,
                    Since = rf.Since,
                    Until = rf.Until
                })
                .FirstOrDefaultAsync();
            
            return restaurantFoodDTO;
        }
        */
    }
}