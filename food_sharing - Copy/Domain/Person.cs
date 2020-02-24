using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace Domain
{
    public class Person
    {
        [Required] public int PersonId { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }
        
        [MaxLength(32)]
        public string? NationalIdentificationNumber { get; set; }
        
        [Display(Name = "Day of birth")]
        public DateTime? Since { get; set; }
        
        [Display(Name = "Day of death")]
        public DateTime? Until { get; set; }
        
        [Required]
        [MaxLength(16)]
        public string Phone { get; set; }
        
    }
}