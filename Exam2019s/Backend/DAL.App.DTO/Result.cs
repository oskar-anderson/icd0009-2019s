using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Result : DomainEntityIdMetadata
    {
        public int Score { get; set; } = default!;
        
        public Guid QuizId { get; set; } = default!;
        public Quiz? Quiz { get; set; }
        
        public string QuestionToPickedAnswer { get; set; } = default!;
        
        public string PersonName { get; set; } = default!;
    }
}