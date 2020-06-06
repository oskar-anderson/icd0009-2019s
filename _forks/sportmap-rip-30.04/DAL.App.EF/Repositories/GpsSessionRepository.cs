using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class GpsSessionRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.GpsSession, DAL.App.DTO.GpsSession>,
        IGpsSessionRepository
    {
        public GpsSessionRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new DALMapper<Domain.App.GpsSession, DTO.GpsSession>())
        {
        }

        public override async Task<IEnumerable<GpsSession>> GetAllAsyncBase(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(g => g.AppUser)
                .Include(g => g.GpsSessionType)
                .ThenInclude(g => g!.Name)
                .ThenInclude(t => t!.Translations);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<IEnumerable<GpsSessionView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(a => a.GpsSessionType)
                .ThenInclude(a => a!.Name)
                .ThenInclude(a => a!.Translations)
                .Select(a => new GpsSessionView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    RecordedAt = a.RecordedAt,
                    Duration = a.Duration,
                    Speed = a.Speed,
                    Distance = a.Distance,
                    Climb = a.Climb,
                    Descent = a.Descent,
                    MinSpeed = a.PaceMin,
                    MaxSpeed = a.PaceMax,
                    GpsLocationsCount = a.GpsLocations!.Count,
                    GpsSessionType = a.GpsSessionType!.Name,
                    UserFirstLastName = a.AppUser!.FirstName + " " + a.AppUser!.LastName,
                }).ToListAsync();
        }
    }
}