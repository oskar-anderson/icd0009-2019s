﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        [Required] public int FriendId { get; set; }
        public virtual Friend Friend { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required] public decimal Percent { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal Calculation
        {
            get {return Calculation;}
            set { Calculation = CalculatePersonSharePrice(Item); }
        }


        public decimal CalculatePersonSharePrice(Item item)
        {
            return (decimal) item.Gross * Percent;
        }
        
    }
}