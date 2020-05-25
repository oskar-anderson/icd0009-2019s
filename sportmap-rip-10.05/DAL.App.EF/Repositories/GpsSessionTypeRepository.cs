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
        public GpsSessionTypeRepository(AppDbContext repoDbContext) : base(repoDbContext,  new DALMapper<Domain.App.GpsSessionType, DTO.GpsSessionType>())
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
    }
}