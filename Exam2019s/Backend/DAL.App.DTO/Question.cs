using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Question : DomainEntityIdMetadata
    {
        public int OrderNumber { get; set; } = default!;
        
        public string QuestionName { get; set; } = default!;
        
        public int Points { get; set; } = default!;
        
        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }
        
        public ICollection<Choice>? Choices { get; set; }
    }
}