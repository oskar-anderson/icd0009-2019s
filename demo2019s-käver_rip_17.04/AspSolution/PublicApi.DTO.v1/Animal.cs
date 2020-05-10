using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Animal : AnimalEdit
    {
        public int OwnerCount { get; set; }
    }
    
    public class AnimalCreate
    {
        [MinLength(1)] [MaxLength(64)] 
        public string AnimalName { get; set; } = default!;
        public int? BirthYear { get; set; }
    }
    
    public class AnimalEdit : AnimalCreate
    {
        public Guid Id { get; set; }
    }
    
}