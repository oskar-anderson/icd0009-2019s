using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ItemServiceMapper : BaseMapper<Item, BLLAppDTO.Item>, IItemServiceMapper
    {
        public ItemServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<Item, BLLAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<InvoiceLine, BLLAppDTO.InvoiceLine>();
            MapperConfigurationExpression.CreateMap<Sharing, BLLAppDTO.Sharing>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.Item MapItemView(Item inObject)
        {
            return Mapper.Map<BLLAppDTO.Item>(inObject);
        }
    }
}