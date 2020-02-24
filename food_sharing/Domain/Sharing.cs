using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Sharing
    {
        [Required] public int SharingId { get; set; }
        
        [Required] public int UserId { get; set; }
        public virtual User User { get; set; }
        
        public virtual ICollection<SharingItem> SharingItems { get; set; } 
    }
}