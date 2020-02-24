using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Domain
{
    public class SharingItem
    {
        [Required] public int SharingItemId { get; set; }
        
        [Required] public int SharingId { get; set; }
        public virtual Sharing Sharing { get; set; }

        [Required] public int ItemId { get; set; }
        public virtual Item Item { get; set; } 
        
        [Required] public int UserFriendId { get; set; }
        public virtual UserFriend UserFriend { get; set; }
        
    }
}