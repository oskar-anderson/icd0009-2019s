using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class SharingItemMapper : BaseMapper<BLL.App.DTO.SharingItem, SharingItemDTO>
    {
        public SharingItemMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.SharingItem, SharingItemDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Sharing, SharingDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, ItemDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public SharingItemDTO MapSharingItemView(BLL.App.DTO.SharingItem inObject)
        {
            return Mapper.Map<SharingItemDTO>(inObject);
        }
    }
}