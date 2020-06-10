using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ChoiceMapper : BaseMapper<DAL.App.DTO.Choice, ChoiceDTO>
    {
        public ChoiceMapper():base()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Choice, ChoiceDTO>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Question, QuestionDTO>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Quiz, QuizDTO>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public ChoiceDTO MapChoiceView(DAL.App.DTO.Choice inObject)
        {
            return Mapper.Map<ChoiceDTO>(inObject);
        }
    }
}