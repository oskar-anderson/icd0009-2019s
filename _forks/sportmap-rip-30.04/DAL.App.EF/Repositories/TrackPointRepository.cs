using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class TrackPointRepository:
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.TrackPoint, DAL.App.DTO.TrackPoint>,
        ITrackPointRepository
    {
        public TrackPointRepository(AppDbContext repoDbContext) : base(repoDbContext, new BaseMapper<Domain.App.TrackPoint, DTO.TrackPoint>())
        {
        }
    }
}