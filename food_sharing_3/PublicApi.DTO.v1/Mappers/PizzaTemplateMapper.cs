using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class PizzaTemplateMapper : BaseMapper<BLL.App.DTO.PizzaTemplate, PizzaTemplateDTO>
    {
        public PizzaTemplateMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PizzaTemplate, PizzaTemplateDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, CategoryDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public PizzaTemplateDTO MapPizzaTemplateView(BLL.App.DTO.PizzaTemplate inObject)
        {
            return Mapper.Map<PizzaTemplateDTO>(inObject);
        }
    }
}