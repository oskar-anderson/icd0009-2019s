using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;

namespace BLL.App.Services
{
    public class TrackService :
        BaseEntityService<IAppUnitOfWork, ITrackRepository, ITrackServiceMapper,
            DAL.App.DTO.Track, BLL.App.DTO.Track>, ITrackService
    {
        public TrackService(IAppUnitOfWork uow) : base(uow, uow.Tracks, new TrackServiceMapper())
        {
        }
    }
}