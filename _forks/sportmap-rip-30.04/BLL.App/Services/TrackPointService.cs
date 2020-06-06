using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;

namespace BLL.App.Services
{
    public class TrackPointService :
        BaseEntityService<IAppUnitOfWork, ITrackPointRepository, ITrackPointServiceMapper,
            DAL.App.DTO.TrackPoint, BLL.App.DTO.TrackPoint>, ITrackPointService
    {
        public TrackPointService(IAppUnitOfWork uow) : base(uow, uow.TrackPoints, new TrackPointServiceMapper())
        {
        }
    }
}