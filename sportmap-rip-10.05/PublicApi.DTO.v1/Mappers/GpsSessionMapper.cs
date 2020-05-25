using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class GpsSessionMapper : BaseMapper<BLL.App.DTO.GpsSession, GpsSession>
    {
        public GpsSessionMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.GpsSessionView, GpsSessionView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public GpsSessionView MapGpsSessionView(BLL.App.DTO.GpsSessionView inObject)
        {
            return Mapper.Map<GpsSessionView>(inObject);
        }
    }
}