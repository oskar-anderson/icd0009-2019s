using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PizzaComponentServiceMapper : BaseMapper<PizzaComponent, BLLAppDTO.PizzaComponent>, IPizzaComponentServiceMapper
    {
        public PizzaComponentServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<PizzaComponent, BLLAppDTO.PizzaComponent>();
            MapperConfigurationExpression.CreateMap<Component, BLLAppDTO.Component>();
            MapperConfigurationExpression.CreateMap<PizzaTemplate, BLLAppDTO.PizzaTemplate>();
            MapperConfigurationExpression.CreateMap<PizzaFinal, BLLAppDTO.PizzaFinal>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.PizzaComponent MapPizzaComponentView(PizzaComponent inObject)
        {
            return Mapper.Map<BLLAppDTO.PizzaComponent>(inObject);
        }
    }
}