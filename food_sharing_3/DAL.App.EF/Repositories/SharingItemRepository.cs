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
    public class SharingItemRepository : 
        EFBaseRepository<AppDbContext, AppUser, Domain.App.SharingItem, SharingItem>, 
        ISharingItemRepository
    {
        public SharingItemRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.App.SharingItem, SharingItem>())
        {
        }

        /*
        public async Task<IEnumerable<SharingItem>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .Include(si => si.Item)
                .Include(si => si.Sharing)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(si => si.Sharing.AppUserId == userId);
            }

            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<SharingItem> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(si => si.Item)
                .Include(si => si.Sharing)
                .Where(si => si.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(si => si.Sharing.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(cm => cm.Id == id);
            }

            return await RepoDbSet.AnyAsync(si => si.Sharing.AppUserId == userId);

        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var sharingItem = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(sharingItem.Id);
        }
        */
        public virtual async Task<IEnumerable<SharingItem>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Select(si => new SharingItem()
                {
                    Id = si.Id,
                    ItemId = si.ItemId,
                    Item = new Item()
                        {
                            Id = si.Item.Id,
                            SharingId = si.Item.SharingId,
                            Sharing = new Sharing()
                            {
                                Id = si.Item.Sharing.Id,
                                AppUserId = si.Item.Sharing.AppUserId,
                                Name = si.Item.Sharing.Name
                            },
                            Name = si.Item.Name,
                            Gross = si.Item.Gross,
                        },
                    SharingId = si.SharingId,
                    Sharing = new Sharing()
                        {
                            Id = si.Sharing.Id,
                            AppUserId = si.Sharing.AppUserId,
                            Name = si.Sharing.Name
                        },
                    FriendName = si.FriendName,
                    Percent = si.Percent,
                    FriendOwns = si.FriendOwns,
                })
                .ToListAsync();
        }

        public virtual async Task<SharingItem> FirstOrDefaultViewAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(si => si.Id == id).AsQueryable();

            return await query
                .Select(si => new SharingItem()
                {
                    Id = si.Id,
                    ItemId = si.ItemId,
                    Item = new Item()
                        {
                            Id = si.Item.Id,
                            SharingId = si.Item.SharingId,
                            Sharing = new Sharing()
                            {
                                Id = si.Item.Sharing.Id,
                                AppUserId = si.Item.Sharing.AppUserId,
                                Name = si.Item.Sharing.Name
                            },
                            Name = si.Item.Name,
                            Gross = si.Item.Gross,
                        },
                    SharingId = si.SharingId,
                    Sharing = new Sharing()
                        {
                            Id = si.Sharing.Id,
                            AppUserId = si.Sharing.AppUserId,
                            Name = si.Sharing.Name
                        },
                    FriendName = si.FriendName,
                    Percent = si.Percent,
                    FriendOwns = si.FriendOwns,
                })
                .FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<SharingItem>> GetAllForApiAsync()
        {
            return await RepoDbSet
                .Select(si => new SharingItem()
                {
                    Id = si.Id,
                    ItemId = si.ItemId,
                    Item = new Item()
                        {
                            Id = si.Item.Id,
                            SharingId = si.Item.SharingId,
                            Sharing = new Sharing()
                            {
                                Id = si.Item.Sharing.Id,
                                AppUserId = si.Item.Sharing.AppUserId,
                                Name = si.Item.Sharing.Name
                            },
                            Name = si.Item.Name,
                            Gross = si.Item.Gross,
                        },
                    SharingId = si.SharingId,
                    Sharing = new Sharing()
                        {
                            Id = si.Sharing.Id,
                            AppUserId = si.Sharing.AppUserId,
                            Name = si.Sharing.Name
                        },
                    FriendName = si.FriendName,
                    Percent = si.Percent,
                    FriendOwns = si.FriendOwns,
                })
                .ToListAsync();
        }

        public virtual async Task<SharingItem> FirstOrDefaultApiAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(si => si.Id == id).AsQueryable();

            return await query
                .Select(si => new SharingItem()
                {
                    Id = si.Id,
                    ItemId = si.ItemId,
                    Item = new Item()
                        {
                            Id = si.Item.Id,
                            SharingId = si.Item.SharingId,
                            Sharing = new Sharing()
                            {
                                Id = si.Item.Sharing.Id,
                                AppUserId = si.Item.Sharing.AppUserId,
                                Name = si.Item.Sharing.Name
                            },
                            Name = si.Item.Name,
                            Gross = si.Item.Gross,
                        },
                    SharingId = si.SharingId,
                    Sharing = new Sharing()
                        {
                            Id = si.Sharing.Id,
                            AppUserId = si.Sharing.AppUserId,
                            Name = si.Sharing.Name
                        },
                    FriendName = si.FriendName,
                    Percent = si.Percent,
                    FriendOwns = si.FriendOwns,
                })
                .FirstOrDefaultAsync();
        }
    }
}