using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppRole : IdentityRole<Guid>, IDomainEntityId 
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string DisplayName { get; set; } = default!;
    }
}