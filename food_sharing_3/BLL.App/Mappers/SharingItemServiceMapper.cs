using AutoMapper;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using ee.itcollege.kaande.pitsariina.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class SharingItemServiceMapper : BaseMapper<SharingItem, BLLAppDTO.SharingItem>, ISharingItemServiceMapper
    {
        public SharingItemServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<SharingItem, BLLAppDTO.SharingItem>();
            MapperConfigurationExpression.CreateMap<Sharing, BLLAppDTO.Sharing>();
            MapperConfigurationExpression.CreateMap<Item, BLLAppDTO.Item>();
            MapperConfigurationExpression.CreateMap<AppUser, BLLAppDTO.Identity.AppUser>();
            
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.SharingItem MapSharingItemView(SharingItem inObject)
        {
            return Mapper.Map<BLLAppDTO.SharingItem>(inObject);
        }
    }
}