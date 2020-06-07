using AutoMapper;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using ee.itcollege.kaande.pitsariina.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class UserLocationServiceMapper : BaseMapper<UserLocation, BLLAppDTO.UserLocation>, IUserLocationServiceMapper
    {
        public UserLocationServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<UserLocation, BLLAppDTO.UserLocation>();
            MapperConfigurationExpression.CreateMap<AppUser, BLLAppDTO.Identity.AppUser>();
            // add more mappings

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public BLLAppDTO.UserLocation MapUserLocationView(UserLocation inObject)
        {
            return Mapper.Map<BLLAppDTO.UserLocation>(inObject);
        }
    }
}