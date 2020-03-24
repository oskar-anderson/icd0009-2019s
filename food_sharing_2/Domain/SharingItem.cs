﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
 using DAL.Base;

 namespace Domain
{
    public class SharingItem : DomainEntity
    {
        [MaxLength(32)] public string SharingId { get; set; } = default!;
        public virtual Sharing? Sharing { get; set; }

        [MaxLength(32)] public string ItemId { get; set; } = default!;
        public virtual Item? Item { get; set; }

        [MaxLength(32)] public string FriendId { get; set; } = default!;
        public virtual Friend? Friend { get; set; }

        [Column(TypeName = "decimal(18,4)")] public decimal Percent { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")]
        
        public decimal FriendOwns => CalculatePersonSharePrice(Item);

        public decimal CalculatePersonSharePrice(Item item)
        {
            return (decimal) item.Gross * Percent;
        }
        
    }
}