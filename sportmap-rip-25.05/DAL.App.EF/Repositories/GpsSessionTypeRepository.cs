using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using GpsSessionType = DAL.App.DTO.GpsSessionType;

namespace DAL.App.EF.Repositories
{
    public class GpsSessionTypeRepository:
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.GpsSessionType, DAL.App.DTO.GpsSessionType>,
        IGpsSessionTypeRepository
    {
        public GpsSessionTypeRepository(AppDbContext repoDbContext) : base(repoDbContext,  new  DALMapper<Domain.App.GpsSessionType, DTO.GpsSessionType>())
        {
        }

        public override async Task<IEnumerable<GpsSessionType>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.Name)
                .ThenInclude(t => t!.Translations)
                .Include(l => l.Description)
                .ThenInclude(t => t!.Translations);
            
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<GpsSessionType> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                .Include(l => l.Name)
                .ThenInclude(t => t!.Translations)
                .Include(l => l.Description)
                .ThenInclude(t => t!.Translations)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            
            var result = Mapper.Map(domainEntity);
            return result;
        }


        public override async Task<GpsSessionType> UpdateAsync(GpsSessionType entity, object? userId = null)
        {
            var domainEntity = Mapper.Map(entity);

            // fix the language string - from mapper we get new ones - so duplicate values will be created in db
            // load back from db the originals 
            domainEntity.Name = await RepoDbContext.LangStrs.Include(t => t.Translations).FirstAsync(s => s.Id == domainEntity.NameId);
            domainEntity.Name.SetTranslation(entity.Name);

            domainEntity.Description = await RepoDbContext.LangStrs.Include(t => t.Translations).FirstAsync(s => s.Id == domainEntity.DescriptionId);
            domainEntity.Description.SetTranslation(entity.Description);
            
            
            await CheckDomainEntityOwnership(domainEntity, userId);
            
            var trackedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
            var result = Mapper.Map(trackedDomainEntity);
            return result;        }
    }
}