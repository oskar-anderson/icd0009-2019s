using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ComponentPriceServiceMapper : BaseMapper<ComponentPrice, BLLAppDTO.ComponentPrice>, IComponentPriceServiceMapper
    {
        public ComponentPriceServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<ComponentPrice, BLLAppDTO.ComponentPrice>();
            MapperConfigurationExpression.CreateMap<Component, BLLAppDTO.Component>();
            MapperConfigurationExpression.CreateMap<Restaurant, BLLAppDTO.Restaurant>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.ComponentPrice MapComponentPriceView(ComponentPrice inObject)
        {
            return Mapper.Map<BLLAppDTO.ComponentPrice>(inObject);
        }
    }
}