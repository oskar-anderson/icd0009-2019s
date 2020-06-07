using AutoMapper;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using ee.itcollege.kaande.pitsariina.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class SharingServiceMapper : BaseMapper<Sharing, BLLAppDTO.Sharing>, ISharingServiceMapper
    {
        public SharingServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<Sharing, BLLAppDTO.Sharing>();
            MapperConfigurationExpression.CreateMap<AppUser, BLLAppDTO.Identity.AppUser>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.Sharing MapSharingView(Sharing inObject)
        {
            return Mapper.Map<BLLAppDTO.Sharing>(inObject);
        }
    }
}