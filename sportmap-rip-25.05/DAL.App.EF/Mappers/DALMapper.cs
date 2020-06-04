using AutoMapper;
using DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // add more mappings
            MapperConfigurationExpression.CreateMap<Domain.App.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<Domain.App.GpsSessionType, DAL.App.DTO.GpsSessionType>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.GpsSession, DAL.App.DTO.GpsSession>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.GpsSession, Domain.App.GpsSession>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.GpsLocation, DAL.App.DTO.GpsLocation>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.GpsLocation, Domain.App.GpsLocation>();

            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}