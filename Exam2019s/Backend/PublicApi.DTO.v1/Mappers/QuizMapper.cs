using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class QuizMapper : BaseMapper<DAL.App.DTO.Quiz, QuizDTO>
    {
        public QuizMapper():base()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Quiz, QuizDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public QuizDTO MapQuizView(DAL.App.DTO.Quiz inObject)
        {
            return Mapper.Map<QuizDTO>(inObject);
        }
    }
}