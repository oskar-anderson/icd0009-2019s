using Contracts.BLL.Base.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IPizzaFinalServiceMapper: IBaseMapper<PizzaFinal, BLLAppDTO.PizzaFinal>
    {
        BLLAppDTO.PizzaFinal MapPizzaFinalView(PizzaFinal inObject);
    }
}