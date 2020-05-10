using Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=Domain.Base.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface ICartServiceMapper: IBaseMapper<DALAppDTO.Cart, BLLAppDTO.Cart>
    {
        
    }
}