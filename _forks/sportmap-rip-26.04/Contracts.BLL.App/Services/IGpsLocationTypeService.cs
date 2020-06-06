using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IGpsLocationTypeService : IBaseEntityService<GpsLocationType>, IGpsLocationTypeRepositoryCustom<GpsLocationType>
    {
    }
}