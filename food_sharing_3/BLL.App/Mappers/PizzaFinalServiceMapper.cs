using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PizzaFinalServiceMapper : BaseMapper<PizzaFinal, BLLAppDTO.PizzaFinal>, IPizzaFinalServiceMapper
    {
        public PizzaFinalServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<PizzaFinal, BLLAppDTO.PizzaFinal>();
            MapperConfigurationExpression.CreateMap<Pizza, BLLAppDTO.Pizza>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.PizzaFinal MapPizzaFinalView(PizzaFinal inObject)
        {
            return Mapper.Map<BLLAppDTO.PizzaFinal>(inObject);
        }
    }
}