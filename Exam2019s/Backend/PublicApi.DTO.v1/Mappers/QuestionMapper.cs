using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class QuestionMapper : BaseMapper<DAL.App.DTO.Question, QuestionDTO>
    {
        public QuestionMapper():base()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Question, QuestionDTO>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Quiz, QuizDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public QuestionDTO MapQuestionView(DAL.App.DTO.Question inObject)
        {
            return Mapper.Map<QuestionDTO>(inObject);
        }
    }
}