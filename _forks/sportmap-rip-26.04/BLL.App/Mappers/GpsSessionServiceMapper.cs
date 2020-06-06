using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class GpsSessionServiceMapper: BaseMapper<DALAppDTO.GpsSession, BLLAppDTO.GpsSession>, IGpsSessionServiceMapper
    {
        public GpsSessionServiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.GpsSessionView, BLLAppDTO.GpsSessionView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.GpsSessionView MapGpsSessionView(DALAppDTO.GpsSessionView inObject)
        {
            return Mapper.Map<BLLAppDTO.GpsSessionView>(inObject);
        }
    }
}