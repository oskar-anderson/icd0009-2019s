﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class UserLocationRepository :  EFBaseRepository<UserLocation, AppDbContext>, IUserLocationRepository
    {
        public UserLocationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<UserLocation>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(ul => ul.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(ul => ul.AppUserId == userId);
            }

            return await query.ToListAsync();
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

            return await query.FirstOrDefaultAsync();
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
            base.Remove(userLocation);
        }

        public async Task<IEnumerable<UserLocationDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(ul => new UserLocationDTO()
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

        public async Task<UserLocationDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            UserLocationDTO userLocationDTO = await query
                .Select(ul => new UserLocationDTO()
                {
                    Id = ul.Id,
                    AppUserId = ul.AppUserId,
                    District = ul.District,
                    StreetName = ul.StreetName,
                    BuildingNumber = ul.BuildingNumber,
                    ApartmentNumber = ul.ApartmentNumber,
                })
                .FirstOrDefaultAsync();
            
            return userLocationDTO;
        }
    }
}