using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface ISharingServiceMapper: IBaseMapper<Sharing, BLLAppDTO.Sharing>
    {
        BLLAppDTO.Sharing MapSharingView(Sharing inObject);
    }
}