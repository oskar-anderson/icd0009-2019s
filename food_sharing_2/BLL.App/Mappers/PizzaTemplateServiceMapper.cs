using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=Domain.Base.App.DTO;
namespace BLL.App.Mappers
{
    public class PizzaTemplateServiceMapper : BaseMapper<DALAppDTO.PizzaTemplate, BLLAppDTO.PizzaTemplate>, IPizzaTemplateServiceMapper
    {
        public PizzaTemplateServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Category, BLLAppDTO.Category>();
            // add more mappings
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.Category MapGpsSessionView(DALAppDTO.Category inObject)
        {
            return Mapper.Map<BLLAppDTO.Category>(inObject);
        }

    }
}