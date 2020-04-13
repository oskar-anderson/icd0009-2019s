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
    public class AnimalRepository : EFBaseRepository<Animal, AppDbContext>, IAnimalRepository
    {
        public AnimalRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Animal>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync();
        }

        public async Task<Animal> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }
       
        #region DTO methods
        public async Task<IEnumerable<AnimalDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }
            return await query
                .Select(o => new AnimalDTO()
                {
                    Id = o.Id,
                    AnimalName = o.AnimalName,
                    BirthYear = o.BirthYear,
                    OwnerCount = o.Owners!.Count,
                })
                .ToListAsync();
        }

        public async Task<AnimalDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var animalDTO = await query.Select(o => new AnimalDTO()
            {
                Id = o.Id,
                AnimalName = o.AnimalName,
                BirthYear = o.BirthYear,
                OwnerCount = o.Owners!.Count
            }).FirstOrDefaultAsync();

            return animalDTO;
        }
        #endregion
    }
}