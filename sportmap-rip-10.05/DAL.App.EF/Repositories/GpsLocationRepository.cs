using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GpsLocationRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.GpsLocation, DAL.App.DTO.GpsLocation>,
        IGpsLocationRepository
    {
        public GpsLocationRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.GpsLocation, DTO.GpsLocation>())
        {
        }

        public virtual async Task<IEnumerable<DTO.GpsLocation>> GetAllAsync(Guid gpsSessionId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Where(e => e.GpsSessionId == gpsSessionId)
                .OrderBy(e => e.RecordedAt);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<GpsLocation> LastInSessionAsync(Guid gpsSessionId, bool noTracking = true)
        {
            var query = PrepareQuery(null, noTracking);
            query = query.Where(a => a.GpsSessionId == gpsSessionId).OrderBy(a => a.RecordedAt);
            var domainEntity = await query.FirstOrDefaultAsync();
            return Mapper.Map(domainEntity);
        }
    }
}