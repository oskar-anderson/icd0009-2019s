using AutoMapper;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using ee.itcollege.kaande.pitsariina.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PizzaTemplateServiceMapper : BaseMapper<PizzaTemplate, BLLAppDTO.PizzaTemplate>, IPizzaTemplateServiceMapper
    {
        public PizzaTemplateServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<PizzaTemplate, BLLAppDTO.PizzaTemplate>();
            MapperConfigurationExpression.CreateMap<Category, BLLAppDTO.Category>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.PizzaTemplate MapPizzaTemplateView(PizzaTemplate inObject)
        {
            return Mapper.Map<BLLAppDTO.PizzaTemplate>(inObject);
        }
    }
}