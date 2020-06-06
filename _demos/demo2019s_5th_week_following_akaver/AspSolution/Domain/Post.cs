using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Post : DomainEntityMetadata
    {
        [MaxLength(4096)] public string Body { get; set; } = default!;
        
        [MaxLength(36)]
        [ForeignKey(nameof(PersonWhoHasWrittenIt))]
        public string PersonWhoHasWrittenItId { get; set; }
        public Author? PersonWhoHasWrittenIt { get; set; }
        
        [MaxLength(36)]
        [ForeignKey(nameof(CoAuthor))]
        public string CoAuthorId { get; set; }
        public Author? CoAuthor { get; set; }
        
        public ICollection<PostCategory>? PostCategories { get; set; } 
        
    }
}