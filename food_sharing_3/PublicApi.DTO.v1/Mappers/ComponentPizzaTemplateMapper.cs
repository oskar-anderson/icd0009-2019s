using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ComponentPizzaTemplateMapper : BaseMapper<BLL.App.DTO.ComponentPizzaTemplate, ComponentPizzaTemplateDTO>
    {
        public ComponentPizzaTemplateMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ComponentPizzaTemplate, ComponentPizzaTemplateDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Component, ComponentDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PizzaTemplate, PizzaTemplateDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, CategoryDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public ComponentPizzaTemplateDTO MapComponentPizzaTemplateView(BLL.App.DTO.ComponentPizzaTemplate inObject)
        {
            return Mapper.Map<ComponentPizzaTemplateDTO>(inObject);
        }
    }
}