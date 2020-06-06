using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GpsLocationTypeRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.GpsLocationType, DAL.App.DTO.GpsLocationType>,
        IGpsLocationTypeRepository
    {
        public GpsLocationTypeRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<Domain.App.GpsLocationType, DTO.GpsLocationType>())
        {
        }

        public override async Task<IEnumerable<GpsLocationType>> GetAllAsyncBase(object? userId = null, bool noTracking = true)
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