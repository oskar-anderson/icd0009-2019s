using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.Identity
{
    public class RegisterDTO
    {
        [MaxLength(256)]
        [EmailAddress]
        [Required]
        public string Email { get; set; } = default!;
        
        [MinLength(6)]
        [MaxLength(100)]
        [Required]
        public string Password { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string FirstName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string LastName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        [Phone]
        [Required]
        public string Phone { get; set; } = default!;
    }
}