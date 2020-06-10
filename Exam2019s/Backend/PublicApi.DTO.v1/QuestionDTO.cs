using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }
        
        [Range(0, 1000)] public int OrderNumber { get; set; } = default!;
        
        [MinLength(1)] [MaxLength(256)] public string QuestionName { get; set; } = default!;
        
        [Range(0, 100)] public int Points { get; set; } = default!;
        
        public Guid QuizId { get; set; } = default!;
        public QuizDTO? Quiz { get; set; }
    }
}