using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Question : DomainEntityIdMetadata
    {
        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }
        [Range(0, 1000)] public int OrderNumber { get; set; } = default!;
        
        [MinLength(1)] [MaxLength(256)] public string QuestionName { get; set; } = default!;
        
        [Range(0, 100)] public int Points { get; set; } = default!;
        
        public ICollection<Choice>? Choices { get; set; }
    }
}