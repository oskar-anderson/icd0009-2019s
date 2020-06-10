using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ResultDTO
    {
        public Guid Id { get; set; }
        
        public int Score { get; set; } = default!;
        
        public Guid QuizId { get; set; } = default!;
        public QuizDTO? Quiz { get; set; }
        
        [MinLength(1)] [MaxLength(4096)] public string QuestionToPickedAnswer { get; set; } = default!;
        
        [MaxLength(64)] public string PersonName { get; set; } = default!;
    }
}