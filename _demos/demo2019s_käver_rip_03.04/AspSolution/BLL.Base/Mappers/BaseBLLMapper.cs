using AutoMapper;
using Contracts.BLL.Base.Mappers;

namespace BLL.Base.Mappers
{
    public class BaseBLLMapper<TInObject, TOutObject> : IBaseBLLMapper<TInObject, TOutObject>
        where TInObject : class, new() 
        where TOutObject : class, new()

    {
        private readonly IMapper _mapper;

        public BaseBLLMapper()
        {
            _mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<TInObject, TOutObject>();
                config.CreateMap<TOutObject, TInObject>();
            }).CreateMapper();
        }
        public TOutObject Map<TInObject, TOutObject>(TInObject inObject) 
            where TInObject : class, new() 
            where TOutObject : class, new()
        {
            return _mapper.Map<TInObject, TOutObject>(inObject);
        }
    }
}