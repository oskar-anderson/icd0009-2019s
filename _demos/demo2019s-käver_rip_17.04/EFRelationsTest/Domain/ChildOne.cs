using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ChildOne
    { 
        public int Id { get; set; }

        [MaxLength(128)] 
        [MinLength(1)] 
        public string Value { get; set; }  = default!;

        public PrimaryOne? PrimaryOne { get; set; }
    }
}