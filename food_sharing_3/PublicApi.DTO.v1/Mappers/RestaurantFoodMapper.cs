using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class RestaurantFoodMapper : BaseMapper<BLL.App.DTO.RestaurantFood, RestaurantFoodDTO>
    {
        public RestaurantFoodMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.RestaurantFood, RestaurantFoodDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Restaurant, RestaurantDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, CategoryDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Pizza, PizzaDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PizzaTemplate, PizzaTemplateDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public RestaurantFoodDTO MapRestaurantFoodView(BLL.App.DTO.RestaurantFood inObject)
        {
            return Mapper.Map<RestaurantFoodDTO>(inObject);
        }
    }
}