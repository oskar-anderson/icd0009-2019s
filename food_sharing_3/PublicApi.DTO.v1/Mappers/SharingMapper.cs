using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class SharingMapper : BaseMapper<BLL.App.DTO.Sharing, SharingDTO>
    {
        public SharingMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Sharing, SharingDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public SharingDTO MapSharingView(BLL.App.DTO.Sharing inObject)
        {
            return Mapper.Map<SharingDTO>(inObject);
        }
    }
}