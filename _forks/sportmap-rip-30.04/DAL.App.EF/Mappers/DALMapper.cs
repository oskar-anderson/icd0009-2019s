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
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}