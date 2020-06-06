using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;

namespace BLL.App.Services
{
    public class GpsLocationTypeService :
        BaseEntityService<IAppUnitOfWork, IGpsLocationTypeRepository, IGpsLocationTypeServiceMapper,
            DAL.App.DTO.GpsLocationType, BLL.App.DTO.GpsLocationType>, IGpsLocationTypeService
    {
        public GpsLocationTypeService(IAppUnitOfWork uow) : base(uow, uow.GpsLocationTypes,
            new GpsLocationTypeServiceMapper())
        {
        }
    }
}