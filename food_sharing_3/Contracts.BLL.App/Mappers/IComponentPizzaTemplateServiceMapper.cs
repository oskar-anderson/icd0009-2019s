using Contracts.BLL.Base.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IComponentPizzaTemplateServiceMapper: IBaseMapper<ComponentPizzaTemplate, BLLAppDTO.ComponentPizzaTemplate>
    {
        BLLAppDTO.ComponentPizzaTemplate MapComponentPizzaTemplateView(ComponentPizzaTemplate inObject);
    }
}