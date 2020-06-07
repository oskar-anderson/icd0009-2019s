using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Domain.App.Identity;
using Domain.Base.App.EF;
using ee.itcollege.kaande.pitsariina.DAL.Base.EF.Repositories;
using ee.itcollege.kaande.pitsariina.DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.Item, Item>, 
        IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.Item, Item>())
        {
        }
        
        /*

        public async Task<IEnumerable<Item>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(i => i.Sharing)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(i => i.Sharing.AppUser.Id == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<Item> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(i => i.Sharing)
                .Where(i => i.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(i => i.Sharing.AppUser.Id == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(i => i.Id == id);
            }

            return await RepoDbSet.AnyAsync(i => i.Sharing.AppUser.Id == userId);

        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var item = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(item.Id);
        }
        */
        public virtual async Task<IEnumerable<Item>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(i => new Item()
                {
                    Id = i.Id,
                    SharingId = i.SharingId,
                    Sharing = new Sharing()
                    {
                        Id = i.Sharing.Id,
                        AppUserId = i.Sharing.AppUserId,
                        Name = i.Sharing.Name
                    },
                    Name = i.Name,
                    Gross = i.Gross,
                })
                .ToListAsync();
        }

        public virtual async Task<Item> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(i => i.Id == id).AsQueryable();
            
            return await query
                .Select(i => new Item()
                {
                    Id = i.Id,
                    SharingId = i.SharingId,
                    Sharing = new Sharing()
                    {
                        Id = i.Sharing.Id,
                        AppUserId = i.Sharing.AppUserId,
                        Name = i.Sharing.Name
                    },
                    Name = i.Name,
                    Gross = i.Gross,
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<Item>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(i => new Item()
                {
                    Id = i.Id,
                    SharingId = i.SharingId,
                    Sharing = new Sharing()
                    {
                        Id = i.Sharing.Id,
                        AppUserId = i.Sharing.AppUserId,
                        Name = i.Sharing.Name
                    },
                    Name = i.Name,
                    Gross = i.Gross,
                })
                .ToListAsync();
        }

        public virtual async Task<Item> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(i => i.Id == id).AsQueryable();
            
            return await query
                .Select(i => new Item()
                {
                    Id = i.Id,
                    SharingId = i.SharingId,
                    Sharing = new Sharing()
                    {
                        Id = i.Sharing.Id,
                        AppUserId = i.Sharing.AppUserId,
                        Name = i.Sharing.Name
                    },
                    Name = i.Name,
                    Gross = i.Gross,
                })
                .FirstOrDefaultAsync();
        }
    }
}