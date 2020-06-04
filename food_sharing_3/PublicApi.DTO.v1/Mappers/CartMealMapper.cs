using AutoMapper;
namespace PublicApi.DTO.v1.Mappers
{
    public class CartMealMapper : BaseMapper<BLL.App.DTO.CartMeal, CartMealDTO>
    {
        public CartMealMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.CartMeal, CartMealDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Cart, CartDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.UserLocation, UserLocationDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Restaurant, RestaurantDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Pizza, PizzaDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PizzaTemplate, PizzaTemplateDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Category, CategoryDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public CartMealDTO MapCartMealView(BLL.App.DTO.CartMeal inObject)
        {
            return Mapper.Map<CartMealDTO>(inObject);
        }
    }
}