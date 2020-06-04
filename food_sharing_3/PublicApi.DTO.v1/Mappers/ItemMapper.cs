using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ItemMapper : BaseMapper<BLL.App.DTO.Item, ItemDTO>
    {
        public ItemMapper():base()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Item, ItemDTO>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Sharing, SharingDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public ItemDTO MapItemView(BLL.App.DTO.Item inObject)
        {
            return Mapper.Map<ItemDTO>(inObject);
        }
    }
}