using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class MealServiceMapper : BaseMapper<Meal, BLLAppDTO.Meal>, IMealServiceMapper
    {
        public MealServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<Meal, BLLAppDTO.Meal>();
            MapperConfigurationExpression.CreateMap<Category, BLLAppDTO.Category>();
            MapperConfigurationExpression.CreateMap<AppUser, BLLAppDTO.Identity.AppUser>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.Meal MapMealView(Meal inObject)
        {
            return Mapper.Map<BLLAppDTO.Meal>(inObject);
        }
    }
}