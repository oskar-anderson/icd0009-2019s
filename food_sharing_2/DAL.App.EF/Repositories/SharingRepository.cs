using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Domain.Base.EF.Repositories;
using Domain.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Base.App.EF.Repositories
{
    public class SharingRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Sharing, DTO.Sharing>, 
        ISharingRepository
    {
        public SharingRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.Sharing, DTO.Sharing>())
        {
        }

        public async Task<IEnumerable<DTO.Sharing>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(s => s.AppUserId == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.Sharing> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(s => s.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(s => s.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(cm => cm.Id == id);
            }

            return await RepoDbSet.AnyAsync(s => s.AppUserId == userId);

        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var sharing = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(sharing.Id);
        }
        
        /*
        public async Task<IEnumerable<SharingDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(s => new SharingDTO()
                {
                    Id = s.Id,
                    AppUserId = s.AppUserId,
                    Name = s.Name,
                    
                })
                .ToListAsync();
        }

        public async Task<SharingDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            SharingDTO sharingDTO = await query
                .Select(s => new SharingDTO()
                {
                    Id = s.Id,
                    AppUserId = s.AppUserId,
                    Name = s.Name,
                })
                .FirstOrDefaultAsync();
            
            return sharingDTO;
        }
        */
    }
}