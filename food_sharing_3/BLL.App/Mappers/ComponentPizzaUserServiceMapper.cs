using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ComponentPizzaUserServiceMapper : BaseMapper<ComponentPizzaUser, BLLAppDTO.ComponentPizzaUser>, IComponentPizzaUserServiceMapper
    {
        public ComponentPizzaUserServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<ComponentPizzaUser, BLLAppDTO.ComponentPizzaUser>();
            MapperConfigurationExpression.CreateMap<Component, BLLAppDTO.Component>();
            MapperConfigurationExpression.CreateMap<PizzaUser, BLLAppDTO.PizzaUser>();
            MapperConfigurationExpression.CreateMap<Pizza, BLLAppDTO.Pizza>();
            MapperConfigurationExpression.CreateMap<PizzaTemplate, BLLAppDTO.PizzaTemplate>();
            MapperConfigurationExpression.CreateMap<Category, BLLAppDTO.Category>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.ComponentPizzaUser MapComponentPizzaUserView(ComponentPizzaUser inObject)
        {
            return Mapper.Map<BLLAppDTO.ComponentPizzaUser>(inObject);
        }
    }
}