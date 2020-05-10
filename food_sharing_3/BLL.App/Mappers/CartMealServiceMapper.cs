using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CartMealServiceMapper : BaseMapper<CartMeal, BLLAppDTO.CartMeal>, ICartMealServiceMapper
    {
        public CartMealServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<CartMeal, BLLAppDTO.CartMeal>();
            MapperConfigurationExpression.CreateMap<Cart, BLLAppDTO.Cart>();
            MapperConfigurationExpression.CreateMap<PizzaFinal, BLLAppDTO.PizzaFinal>();
            MapperConfigurationExpression.CreateMap<Meal, BLLAppDTO.Meal>();
            // add more mappings
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
            
        }
        public BLLAppDTO.CartMeal MapCartMealView(CartMeal inObject)
        {
            return Mapper.Map<BLLAppDTO.CartMeal>(inObject);
        }
    }
}