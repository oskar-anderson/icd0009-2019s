using AutoMapper;
using BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        { 
            // add more mappings
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.GpsSession, BLL.App.DTO.GpsSession>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.GpsSession, DAL.App.DTO.GpsSession>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}