using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;

namespace Domain
{
    public class Post : DomainEntityMetadata
    {
        
        [MaxLength(4096)] 
        [MinLength(1)]
        public string Body { get; set; } = default!;

        [ForeignKey(nameof(PersonWhoHasWrittenIt))]
        [MaxLength(36)]
        public string PersonWhoHasWrittenItId { get; set; } = default!;
        public Author? PersonWhoHasWrittenIt { get; set; }

        [ForeignKey(nameof(CoAuthor))]
        [MaxLength(36)]
        public string CoAuthorId { get; set; } = default!;
        public Author? CoAuthor { get; set; }

        public ICollection<PostCategory>? PostCategories { get; set; }
        
    }
}