using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ResultMapper : BaseMapper<DAL.App.DTO.Result, ResultDTO>
    {
        public ResultMapper():base()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Result, ResultDTO>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Quiz, QuizDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public ResultDTO MapResultView(DAL.App.DTO.Result inObject)
        {
            return Mapper.Map<ResultDTO>(inObject);
        }
    }
}