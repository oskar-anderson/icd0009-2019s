using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Choice : DomainEntityIdMetadata
    {
        public string Name { get; set; } = default!;
        
        public bool GivesPoints { get; set; } = default!;
        public Guid QuestionId { get; set; } = default!;
        public Question? Question { get; set; }
    }
}