using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Item
    {
        [Required] public int ItemId { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        
        [Required]
        public float Price { get; set; }
    }
}