using System;
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

        public override async Task<IEnumerable<GpsSession>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(g => g.AppUser)
                .Include(g => g.GpsSessionType)
                .ThenInclude(g => g!.Name)
                .ThenInclude(t => t!.Translations)
                .OrderByDescending(a => a.RecordedAt);
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<IEnumerable<GpsSessionView>> GetAllForViewAsync(int minLocationsCount = 10,
            double minDuration = 60, double minDistance = 10, DateTime? fromDateTime = null,
            DateTime? toDateTime = null)
        {
            var query = RepoDbSet
                .Include(a => a.GpsSessionType)
                .ThenInclude(a => a!.Name)
                .ThenInclude(a => a!.Translations)
                .Where(a => a.GpsLocations!.Count >= minLocationsCount && a.Duration >= minDuration &&
                            a.Distance >= minDistance);
            if (fromDateTime != null)
            {
                query = query.Where(a => a.RecordedAt >= fromDateTime);
            }

            if (toDateTime != null)
            {
                query = query.Where(a => a.RecordedAt <= toDateTime);
            }

            query = query
                .OrderByDescending(a => a.RecordedAt);
            var result = await query
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
                    UserId = a.AppUserId,
                }).ToListAsync();

            return result;
        }

        public async Task<GpsSession> GetFirstWithAllLocationsAsync(Guid id)
        {
            var domainGpsSession = await RepoDbSet.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            domainGpsSession.GpsLocations = await RepoDbContext.GpsLocations.AsNoTracking()
                .Where(l => l.GpsSessionId == id)
                .OrderByDescending(l => l.RecordedAt)
                .ToListAsync();

            return Mapper.Map(domainGpsSession);
        }
    }
}