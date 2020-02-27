﻿using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ClientGroup
    {
        [Required] public int ClientGroupId { get; set; }
        
        [Required] 
        [MaxLength(64)]
        public string Name { get; set; }
        
        [Required] 
        [MaxLength(4024)]
        public string Description { get; set; }
    }
}