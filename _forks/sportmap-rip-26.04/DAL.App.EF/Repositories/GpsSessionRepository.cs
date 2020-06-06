using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class GpsSessionRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.GpsSession, DAL.App.DTO.GpsSession>,
        IGpsSessionRepository
    {
        public GpsSessionRepository(AppDbContext repoDbContext) : base(repoDbContext,
            new BaseMapper<Domain.GpsSession, DTO.GpsSession>())
        {
        }

        public virtual async Task<IEnumerable<GpsSessionView>> GetAllForViewAsync()
        {
            return await RepoDbSet.Select(a => new GpsSessionView()
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
                UserFirstLastName = a.AppUser!.FirstName + " " + a.AppUser!.LastName,
            }).ToListAsync();
        }
    }
}