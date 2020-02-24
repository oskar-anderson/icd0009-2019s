using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Restaurant
    {
        [Required] public int RestaurantId { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Location { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Telephone { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string OpenTime { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string OpenNotification { get; set; }
        
        public int ? MenuId { get; set; }
        public virtual Menu ? Menu { get; set; }
        
        /*
        Pitsa Riina
        Endla 76
        5724871
        E-P, 12:00-23:00
        10., 12. aprill, 1., 31. mai
        */
    }
}