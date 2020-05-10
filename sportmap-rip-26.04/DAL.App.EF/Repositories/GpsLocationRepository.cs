using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GpsLocationRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.GpsLocation, DAL.App.DTO.GpsLocation>,
        IGpsLocationRepository
    {
        public GpsLocationRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<GpsLocation, DTO.GpsLocation>())
        {
        }

        public virtual async Task<IEnumerable<DTO.GpsLocation>> GetAllAsync(Guid gpsSessionId, Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query.Where(e => e.GpsSessionId == gpsSessionId);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
    }
}