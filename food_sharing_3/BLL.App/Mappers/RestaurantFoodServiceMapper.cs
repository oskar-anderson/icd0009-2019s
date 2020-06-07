using AutoMapper;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class RestaurantFoodServiceMapper : BaseMapper<RestaurantFood, BLLAppDTO.RestaurantFood>, IRestaurantFoodServiceMapper
    {
        public RestaurantFoodServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<RestaurantFood, BLLAppDTO.RestaurantFood>();
            MapperConfigurationExpression.CreateMap<Restaurant, BLLAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<Category, BLLAppDTO.Category>();
            MapperConfigurationExpression.CreateMap<Pizza, BLLAppDTO.Pizza>();
            MapperConfigurationExpression.CreateMap<PizzaTemplate, BLLAppDTO.PizzaTemplate>();

            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.RestaurantFood MapRestaurantFoodView(RestaurantFood inObject)
        {
            return Mapper.Map<BLLAppDTO.RestaurantFood>(inObject);
        }
    }
}