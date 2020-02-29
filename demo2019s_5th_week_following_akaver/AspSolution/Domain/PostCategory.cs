using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class PostCategory : DomainEntityMetadata
    {
        [MaxLength(36)]
        public string PostId { get; set; }
        public Post? Post { get; set; }
        
        [MaxLength(36)]
        public string CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}