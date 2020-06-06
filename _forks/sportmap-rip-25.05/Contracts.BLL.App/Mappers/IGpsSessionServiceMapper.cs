using Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IGpsSessionServiceMapper : IBaseMapper<DALAppDTO.GpsSession, BLLAppDTO.GpsSession>
    {
        BLLAppDTO.GpsSessionView MapGpsSessionView(DALAppDTO.GpsSessionView inObject);
    }
}