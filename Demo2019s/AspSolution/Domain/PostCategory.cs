using DAL.Base;

namespace Domain
{
    public class PostCategory : DomainEntityMetadata
    {
        public int PostId { get; set; }
        public Post? Post { get; set; }
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}