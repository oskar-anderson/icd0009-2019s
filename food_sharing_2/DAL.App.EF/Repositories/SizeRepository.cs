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
    public class SizeRepository :  EFBaseRepository<Size, AppDbContext>, ISizeRepository
    {
        public SizeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Size>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .AsQueryable();
            
            return await query.ToListAsync();
        }

        public async Task<Size> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(s => s.Id == id)
                .AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet
                .AnyAsync(m => m.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var size = await FirstOrDefaultAsync(id, userId);
            base.Remove(size);
        }

        public async Task<IEnumerable<SizeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            return await query
                .Select(s => new SizeDTO()
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToListAsync();
        }

        public async Task<SizeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(s => s.Id == id).AsQueryable();
            SizeDTO sizeDTO = await query.Select(s => new SizeDTO()
            {
                Id = s.Id,
                Name = s.Name,
            }).FirstOrDefaultAsync();

            return sizeDTO;
        }
    }
}