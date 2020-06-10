using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Choice : DomainEntityIdMetadata
    {
        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;
        
        public bool GivesPoints { get; set; } = default!;

        public Guid QuestionId { get; set; } = default!;
        public Question? Question { get; set; }
    }
}