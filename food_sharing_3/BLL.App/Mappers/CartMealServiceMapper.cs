using AutoMapper;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using ee.itcollege.kaande.pitsariina.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CartMealServiceMapper : BaseMapper<CartMeal, BLLAppDTO.CartMeal>, ICartMealServiceMapper
    {
        public CartMealServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<CartMeal, BLLAppDTO.CartMeal>();
            MapperConfigurationExpression.CreateMap<Cart, BLLAppDTO.Cart>();
            MapperConfigurationExpression.CreateMap<UserLocation, BLLAppDTO.UserLocation>();
            MapperConfigurationExpression.CreateMap<AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Restaurant, BLLAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<Pizza, BLLAppDTO.Pizza>();
            MapperConfigurationExpression.CreateMap<PizzaTemplate, BLLAppDTO.PizzaTemplate>();
            MapperConfigurationExpression.CreateMap<Category, BLLAppDTO.Category>();
            // add more mappings
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
            
        }
        public BLLAppDTO.CartMeal MapCartMealView(CartMeal inObject)
        {
            return Mapper.Map<BLLAppDTO.CartMeal>(inObject);
        }
    }
}