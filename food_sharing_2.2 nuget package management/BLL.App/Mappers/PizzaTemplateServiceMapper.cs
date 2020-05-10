using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PizzaTemplateServiceMapper : BaseMapper<PizzaTemplate, BLLAppDTO.PizzaTemplate>, IPizzaTemplateServiceMapper
    {
        public PizzaTemplateServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<Category, BLLAppDTO.Category>();
            // add more mappings
            MapperConfigurationExpression.CreateMap<AppUser, BLLAppDTO.Identity.AppUser>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.Category MapGpsSessionView(Category inObject)
        {
            return Mapper.Map<BLLAppDTO.Category>(inObject);
        }

    }
}