using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PizzaUserServiceMapper : BaseMapper<PizzaUser, BLLAppDTO.PizzaUser>, IPizzaUserServiceMapper
    {
        public PizzaUserServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<PizzaUser, BLLAppDTO.PizzaUser>();
            MapperConfigurationExpression.CreateMap<Pizza, BLLAppDTO.Pizza>();
            // add more mappings
            MapperConfigurationExpression.CreateMap<AppUser, BLLAppDTO.Identity.AppUser>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.PizzaUser MapPizzaFinalView(PizzaUser inObject)
        {
            return Mapper.Map<BLLAppDTO.PizzaUser>(inObject);
        }
    }
}