using Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IGpsSessionTypeServiceMapper: IBaseMapper<DALAppDTO.GpsSessionType, BLLAppDTO.GpsSessionType>
    {
        
    }
}