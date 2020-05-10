using Contracts.BLL.Base.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IComponentPriceServiceMapper: IBaseMapper<ComponentPrice, BLLAppDTO.ComponentPrice>
    {
        BLLAppDTO.ComponentPrice MapComponentPriceView(ComponentPrice inObject);
    }
}