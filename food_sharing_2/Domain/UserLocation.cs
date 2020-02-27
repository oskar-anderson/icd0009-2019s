﻿using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class UserLocation
    {
        [Required] public int UserLocationId { get; set; }
        
        [Required] public int UserId { get; set; }
        public virtual User User { get; set; }
        
        [Required] 
        [MaxLength(32)]
        public string District { get; set; }
        
        [Required] 
        [MaxLength(32)]
        public string ApartmentName { get; set; }
        
        [Required] 
        [MaxLength(32)]
        public string StreetName { get; set; }
        
        [Required] 
        [MaxLength(32)]
        public string BuildingOrApartmentNumber { get; set; }
        
    }
}