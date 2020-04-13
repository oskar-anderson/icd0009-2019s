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
    public class OwnerAnimalRepository : EFBaseRepository<OwnerAnimal, AppDbContext>, IOwnerAnimalRepository
    {
        public OwnerAnimalRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<OwnerAnimal>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Animal)
                .Include(a => a.Owner)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(o => o.Owner!.AppUserId == userId && o.Animal!.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<OwnerAnimal> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Animal)
                .Include(a => a.Owner)
                .Where(a => a.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(a => a.Owner!.AppUserId == userId && a.Animal!.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a =>
                a.Id == id && a.Owner!.AppUserId == userId && a.Animal!.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }

        public async Task<IEnumerable<OwnerAnimalDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.Owner)
                .Include(o => o.Animal)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.Animal!.AppUserId == userId && o.Owner!.AppUserId == userId);
            }

            return await query
                .Select(o => new OwnerAnimalDTO()
                {
                    Id = o.Id,
                    AnimalId = o.AnimalId,
                    OwnerId = o.OwnerId,
                    OwnedPercentage = o.OwnedPercentage,
                    Animal = new AnimalDTO()
                    {
                        Id = o.Animal!.Id,
                        AnimalName = o.Animal!.AnimalName,
                        BirthYear = o.Animal!.BirthYear,
                        OwnerCount = o.Animal!.Owners!.Count,
                    },
                    Owner = new OwnerDTO()
                    {
                        Id = o.Owner!.Id,
                        FirstName = o.Owner!.FirstName,
                        LastName = o.Owner!.LastName,
                        AnimalCount = o.Owner!.Animals!.Count
                    }
                })
                .ToListAsync();
        }

        public Task<OwnerAnimalDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}