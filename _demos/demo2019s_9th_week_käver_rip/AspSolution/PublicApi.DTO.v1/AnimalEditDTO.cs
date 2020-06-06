using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class AnimalEditDTO
    {
        public Guid Id { get; set; }
        
        [MinLength(1)] [MaxLength(64)] 
        public string AnimalName { get; set; } = default!;
        public int? BirthYear { get; set; }
    }
}