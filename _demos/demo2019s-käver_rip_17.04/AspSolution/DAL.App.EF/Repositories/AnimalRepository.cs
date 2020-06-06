using System;
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
    public class AnimalRepository : EFBaseRepository<AppDbContext, Domain.Animal, DAL.App.DTO.Animal>, IAnimalRepository
    {
        public AnimalRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.Animal, DAL.App.DTO.Animal>())
        {
        }
        public async Task<IEnumerable<Animal>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return (await RepoDbSet.Where(o => o.AppUserId == userId)
                .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Animal> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
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
       /*
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
        */
    }
}