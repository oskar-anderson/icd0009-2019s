using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class TrackRepository:
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Track, DAL.App.DTO.Track>,
        ITrackRepository
    {
        public TrackRepository(AppDbContext repoDbContext) : base(repoDbContext, new BaseMapper<Domain.App.Track, DTO.Track>())
        {
        }
    }
}