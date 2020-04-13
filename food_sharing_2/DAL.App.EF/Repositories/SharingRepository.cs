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
    public class SharingRepository :  EFBaseRepository<Sharing, AppDbContext>, ISharingRepository
    {
        public SharingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Sharing>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(s => s.AppUserId == userId);
            }

            return await query.ToListAsync();
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

            return await query.FirstOrDefaultAsync();
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
            base.Remove(sharing);
        }

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
    }
}