using AutoMapper;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using ee.itcollege.kaande.pitsariina.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PizzaServiceMapper : BaseMapper<Pizza, BLLAppDTO.Pizza>, IPizzaServiceMapper
    {
        public PizzaServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<Pizza, BLLAppDTO.Pizza>();
            MapperConfigurationExpression.CreateMap<PizzaTemplate, BLLAppDTO.PizzaTemplate>();
            MapperConfigurationExpression.CreateMap<Category, BLLAppDTO.Category>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.Pizza MapPizzaView(Pizza inObject)
        {
            return Mapper.Map<BLLAppDTO.Pizza>(inObject);
        }
    }
}