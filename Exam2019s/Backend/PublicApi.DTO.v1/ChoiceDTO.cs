using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class ChoiceDTO
    {
        public Guid Id { get; set; }
        
        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;
        
        public bool GivesPoints { get; set; } = default!;

        public Guid QuestionId { get; set; } = default!;
        public QuestionDTO? Question { get; set; }
        
    }
}