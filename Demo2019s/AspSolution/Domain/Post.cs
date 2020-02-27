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
        
        [ForeignKey(nameof(PersonWhoHasWrittenIt))]
        public int PersonWhoHasWrittenItId { get; set; }
        public Author? PersonWhoHasWrittenIt { get; set; }
        
        [ForeignKey(nameof(CoAuthor))]
        public int CoAuthorId { get; set; }
        public Author? CoAuthor { get; set; }
        
        public ICollection<PostCategory>? PostCategories { get; set; } 
        
    }
}