using Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=Domain.Base.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IRestaurantServiceMapper: IBaseMapper<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>
    {
        
    }
}