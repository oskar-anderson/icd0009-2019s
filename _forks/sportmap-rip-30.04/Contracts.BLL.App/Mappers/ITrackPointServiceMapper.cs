using Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface ITrackPointServiceMapper: IBaseMapper<DALAppDTO.TrackPoint, BLLAppDTO.TrackPoint>
    {
        
    }
}