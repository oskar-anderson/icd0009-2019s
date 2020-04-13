using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class PrimaryTwo
    {
        public int Id { get; set; }

        [MaxLength(128)] 
        [MinLength(1)] 
        public string Value { get; set; } = default!;
    
        // 1:0-1, Fk in dependent entity
        public ChildTwo? ChildTwo { get; set; }
    }
}