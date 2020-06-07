using AutoMapper;
using ee.itcollege.kaande.pitsariina.DAL.Base.Mappers;

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
            MapperConfigurationExpression.CreateMap<Domain.App.Cart, DAL.App.DTO.Cart>();
            MapperConfigurationExpression.CreateMap<Domain.App.PizzaTemplate, DAL.App.DTO.PizzaTemplate>();
            MapperConfigurationExpression.CreateMap<Domain.App.Category, DAL.App.DTO.Category>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}