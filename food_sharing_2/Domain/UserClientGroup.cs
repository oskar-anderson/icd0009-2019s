﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class UserClientGroup
    {
        [Required] public int UserClientGroupId { get; set; }
        
        [Required] public int UserId { get; set; }
        public virtual User User { get; set; }
        
        [Required] public int ClientGroupId { get; set; }
        public virtual ClientGroup ClientGroup { get; set; }
        
        [Required]
        public DateTime Since { get; set; }
        
        [Required]
        public DateTime Until { get; set; }
        
    }
}