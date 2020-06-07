using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface ICartMealServiceMapper: IBaseMapper<CartMeal, BLLAppDTO.CartMeal>
    {
        BLLAppDTO.CartMeal MapCartMealView(CartMeal inObject);
    }
}