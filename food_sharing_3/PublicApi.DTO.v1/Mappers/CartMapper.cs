using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class CartMapper : BaseMapper<BLL.App.DTO.Cart, CartDTO>
    {
        public CartMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Cart, CartDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Restaurant, RestaurantDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.UserLocation, UserLocationDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public CartDTO MapCartView(BLL.App.DTO.Cart inObject)
        {
            return Mapper.Map<CartDTO>(inObject);
        }
    }
}