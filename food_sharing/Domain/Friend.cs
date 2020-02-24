using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class UserFriend
    {
        public int UserFriendId { get; set; }
        
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        [MaxLength(128)] public string Name { get; set; }
    }
}