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
    public class SharingRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Sharing, Sharing>, 
        ISharingRepository
    {
        public SharingRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.Sharing, Sharing>())
        {
        }

        /*
        public async Task<IEnumerable<Sharing>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
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

        public async Task<Sharing> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
        
        */
        public virtual async Task<IEnumerable<Sharing>> GetAllForViewAsync(Guid userId)
        {
            var query = RepoDbSet.Where(c => c.AppUserId == userId).AsQueryable();
            
            return await query
                .Select(s => new Sharing()
                {
                    Id = s.Id,
                    AppUserId = s.AppUserId,
                    Name = s.Name,
                })
                .ToListAsync();
        }

        public virtual async Task<Sharing> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(s => s.Id == id).AsQueryable();

            return await query
                .Select(s => new Sharing()
                {
                    Id = s.Id,
                    AppUserId = s.AppUserId,
                    Name = s.Name,
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<Sharing>> GetAllForApiAsync(Guid userId)
        {
            var query = RepoDbSet.Where(c => c.AppUserId == userId).AsQueryable();
            
            return await query
                .Select(s => new Sharing()
                {
                    Id = s.Id,
                    AppUserId = s.AppUserId,
                    Name = s.Name,
                })
                .ToListAsync();
        }

        public virtual async Task<Sharing> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(s => s.Id == id).AsQueryable();

            return await query
                .Select(s => new Sharing()
                {
                    Id = s.Id,
                    AppUserId = s.AppUserId,
                    Name = s.Name,
                })
                .FirstOrDefaultAsync();
        }
    }
}