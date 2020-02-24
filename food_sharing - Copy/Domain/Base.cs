using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Base
    {
        [Required] public int BaseId { get; set; }
        
        [MaxLength(32)]
        [Required]
        public string Name { get; set; }


    }
}