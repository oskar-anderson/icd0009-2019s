using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Quiz : DomainEntityIdMetadata
    {
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        [MinLength(1)] [MaxLength(128)] public string Name { get; set; } = default!;
        [MinLength(1)] [MaxLength(1024)] public string Description { get; set; } = default!;
        
        public ICollection<Result>? Results { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}