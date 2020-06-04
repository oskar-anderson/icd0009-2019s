using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class UserLocationMapper : BaseMapper<BLL.App.DTO.UserLocation, UserLocationDTO>
    {
        public UserLocationMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.UserLocation, UserLocationDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public UserLocationDTO MapUserLocationView(BLL.App.DTO.UserLocation inObject)
        {
            return Mapper.Map<UserLocationDTO>(inObject);
        }
    }
}