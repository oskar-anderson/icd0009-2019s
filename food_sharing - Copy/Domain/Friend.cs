using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Friend
    {
        public int FriendId { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        [MaxLength(128)] public string FirstName { get; set; }
        
        [MaxLength(128)] public string LastName { get; set; }
    }
}