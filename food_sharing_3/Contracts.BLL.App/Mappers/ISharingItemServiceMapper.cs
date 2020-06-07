using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface ISharingItemServiceMapper: IBaseMapper<SharingItem, BLLAppDTO.SharingItem>
    {
        BLLAppDTO.SharingItem MapSharingItemView(SharingItem inObject);
    }
}