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
    public class OwnerRepository : EFBaseRepository<AppDbContext, Domain.Owner, DAL.App.DTO.Owner>, IOwnerRepository
    {
        public OwnerRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Owner, DAL.App.DTO.Owner>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.Owner>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return (await RepoDbSet
                    .Where(o => o.AppUserId == userId)
                    .Select(dbEntity => new OwnerDisplay()
                    {
                        Id = dbEntity.Id,
                        FirstName = dbEntity.FirstName, 
                        LastName = dbEntity.LastName,
                        AnimalCount =  dbEntity.Animals!.Count
                    })
                    .ToListAsync())
                .Select(dbEntity => Mapper.Map<OwnerDisplay,DAL.App.DTO.Owner>(dbEntity));
        }

        public async Task<DAL.App.DTO.Owner> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var owner = await query.AsNoTracking().FirstOrDefaultAsync();
            base.Remove(owner.Id);
        }

        /*
        // we need to do it on database level, to avoid unnecessary queries to db 
        public async Task<IEnumerable<OwnerDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }
            return await query
                .Select(o => new OwnerDTO()
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    AnimalCount = o.Animals!.Count
                })
                .ToListAsync();
        }

        public async Task<OwnerDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var ownerDTO = await query.Select(o => new OwnerDTO()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                AnimalCount = o.Animals!.Count
            }).FirstOrDefaultAsync();

            return ownerDTO;
        }
        */
    }
}