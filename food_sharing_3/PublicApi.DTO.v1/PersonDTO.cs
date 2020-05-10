using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public bool ThisIsMe { get; set; } = default!;
        
        [MaxLength(128)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MinLength(1)] [MaxLength(128)] public string LastName { get; set; } = default!;
        
        [MaxLength(32)] public string? NationalIdentificationNumber { get; set; }
        
        [Display(Name = "Day of birth")]
        public DateTime? Since { get; set; }
        
        [Display(Name = "Day of death")]
        public DateTime? Until { get; set; }

        [MinLength(1)] [MaxLength(16)] public string Phone { get; set; } = default!;

    }
}