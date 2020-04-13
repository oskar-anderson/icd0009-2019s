﻿using System;
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
    public class RestaurantRepository :  EFBaseRepository<Restaurant, AppDbContext>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Restaurant>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();

            return await query.ToListAsync();
            
            // return await base.AllAsync();
        }

        public async Task<Restaurant> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(cm => cm.Id == id)
                .AsQueryable();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(r => r.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var restaurant = await FirstOrDefaultAsync(id, userId);
            base.Remove(restaurant);
        }

        public async Task<IEnumerable<RestaurantDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            return await query
                .Select(r => new RestaurantDTO()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Location = r.Location,
                    Telephone = r.Telephone,
                    OpenTime = r.OpenTime,
                    OpenNotification = r.OpenNotification
                })
                .ToListAsync();
        }

        public async Task<RestaurantDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(r => r.Id == id).AsQueryable();
            
            var restaurantDTO = await query.Select(r => new RestaurantDTO()
            {
                Id = r.Id,
                Name = r.Name,
                Location = r.Location,
                Telephone = r.Telephone,
                OpenTime = r.OpenTime,
                OpenNotification = r.OpenNotification
            }).FirstOrDefaultAsync();

            return restaurantDTO;
        }
    }
}