using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PizzaComponentServiceMapper : BaseMapper<PizzaComponent, BLLAppDTO.PizzaComponent>, IPizzaComponentServiceMapper
    {
        
    }
}