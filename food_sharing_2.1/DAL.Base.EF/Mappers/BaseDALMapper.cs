using System.Runtime.InteropServices;
using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.Base.EF.Mappers
{
    public class BaseDALMapper<TInObject, TOutObject> : IBaseDALMapper<TInObject, TOutObject>
        where TInObject : class, new()
        where TOutObject : class, new()
    {
        private readonly IMapper _mapper;

        public BaseDALMapper()
        {
            _mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<TInObject, TOutObject>();
                config.CreateMap<TOutObject, TInObject>();
            }).CreateMapper();
        }

        public TOutObject Map(TInObject inObject)
        {
            return _mapper.Map<TInObject, TOutObject>(inObject);
        }

        public TMapOutObject Map<TMapInObject, TMapOutObject>(TMapInObject inObject)
            where TMapInObject : class where TMapOutObject : class, new()
        {
            return _mapper.Map<TMapInObject, TMapOutObject>(inObject);
        }
    }
}