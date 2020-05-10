using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class PersonServiceMapper : BaseMapper<Person, BLLAppDTO.Person>, IPersonServiceMapper
    {
        public PersonServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<Category, BLLAppDTO.Category>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.Person MapPersonView(Person inObject)
        {
            return Mapper.Map<BLLAppDTO.Person>(inObject);
        }
    }
}