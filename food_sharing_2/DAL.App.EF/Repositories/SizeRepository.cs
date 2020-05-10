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
    public class SizeRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Size, DTO.Size>, 
        ISizeRepository
    {
        public SizeRepository(AppDbContext dbContext) : base(dbContext, 
            new BaseMapper<Domain.Size, DTO.Size>())
        {
        }

        public async Task<IEnumerable<DTO.Size>> GetAllAsync(Guid id, Guid? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet
                .AsQueryable();
            
            return (await query.ToListAsync()).
                Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DTO.Size> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(s => s.Id == id)
                .AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet
                .AnyAsync(m => m.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var size = await FirstOrDefaultAsync(id, userId);
            await base.RemoveAsync(size.Id);
        }
        
        /*
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
        */
    }
}