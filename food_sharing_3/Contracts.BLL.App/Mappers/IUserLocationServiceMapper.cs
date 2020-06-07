using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IUserLocationServiceMapper: IBaseMapper<UserLocation, BLLAppDTO.UserLocation>
    {
        BLLAppDTO.UserLocation MapUserLocationView(UserLocation inObject);
    }
}