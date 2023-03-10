using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using com.akaver.sportmap.Contracts.Domain;

namespace DAL.App.DTO.Identity
{
    public class AppRole : IDomainEntityId 
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string DisplayName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string Name { get; set; } = default!;

    }
}