using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Result : DomainEntityIdMetadata
    {
        public int Score { get; set; } = default!;
        
        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }
        
        [MinLength(1)] [MaxLength(4096)] public string QuestionToPickedAnswer { get; set; } = default!;
        
        [MinLength(1)] [MaxLength(64)] public string PersonName { get; set; } = default!;
    }
}