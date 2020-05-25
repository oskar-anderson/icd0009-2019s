using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ComponentPizzaTemplateServiceMapper : BaseMapper<ComponentPizzaTemplate, BLLAppDTO.ComponentPizzaTemplate>, IComponentPizzaTemplateServiceMapper
    {
        public ComponentPizzaTemplateServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<ComponentPizzaTemplate, BLLAppDTO.ComponentPizzaTemplate>();
            MapperConfigurationExpression.CreateMap<Component, BLLAppDTO.Component>();
            MapperConfigurationExpression.CreateMap<PizzaTemplate, BLLAppDTO.PizzaTemplate>();
            
            MapperConfigurationExpression.CreateMap<Category, BLLAppDTO.Category>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.ComponentPizzaTemplate MapComponentPizzaTemplateView(ComponentPizzaTemplate inObject)
        {
            return Mapper.Map<BLLAppDTO.ComponentPizzaTemplate>(inObject);
        }
    }
}