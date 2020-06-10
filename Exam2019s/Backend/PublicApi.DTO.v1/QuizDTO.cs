using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class QuizDTO
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        [MinLength(1)] [MaxLength(128)] public string Name { get; set; } = default!;
        [MinLength(1)] [MaxLength(1024)] public string Description { get; set; } = default!;
    }
}