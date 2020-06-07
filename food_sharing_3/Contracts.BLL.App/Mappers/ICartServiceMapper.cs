using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface ICartServiceMapper: IBaseMapper<Cart, BLLAppDTO.Cart>
    {
        BLLAppDTO.Cart MapCartView(Cart inObject);
    }
}