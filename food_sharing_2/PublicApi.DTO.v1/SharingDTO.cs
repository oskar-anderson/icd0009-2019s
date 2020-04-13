using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace PublicApi.DTO.v1
{
    public class SharingDTO
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;
    }
}