using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;

namespace BLL.App.Services
{
    public class GpsSessionTypeService :
        BaseEntityService<IAppUnitOfWork, IGpsSessionTypeRepository, IGpsSessionTypeServiceMapper,
            DAL.App.DTO.GpsSessionType, BLL.App.DTO.GpsSessionType>, IGpsSessionTypeService
    {
        public GpsSessionTypeService(IAppUnitOfWork uow) : base(uow, uow.GpsSessionTypes, new GpsSessionTypeServiceMapper())
        {
        }
    }
}