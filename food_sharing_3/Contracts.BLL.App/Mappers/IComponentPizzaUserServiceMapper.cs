using Contracts.BLL.Base.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IComponentPizzaUserServiceMapper: IBaseMapper<ComponentPizzaUser, BLLAppDTO.ComponentPizzaUser>
    {
        BLLAppDTO.ComponentPizzaUser MapComponentPizzaUserView(ComponentPizzaUser inObject);
    }
}