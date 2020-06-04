using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class PizzaMapper : BaseMapper<BLL.App.DTO.Pizza, PizzaDTO>
    {
        public PizzaMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Pizza, PizzaDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PizzaTemplate, PizzaTemplateDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, CategoryDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public PizzaDTO MapPizzaView(BLL.App.DTO.Pizza inObject)
        {
            return Mapper.Map<PizzaDTO>(inObject);
        }
    }
}