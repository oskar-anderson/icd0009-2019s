using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class SharingItem : DomainEntityIdMetadata
    {
        public Guid SharingId { get; set; } = default!;
        public Sharing? Sharing { get; set; }

        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }

        [MaxLength(128)][MinLength(1)] public string FriendName { get; set; } = default!;
        
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Percent { get; set; } = default!;
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal FriendOwns { get; set; } = default!;    // Will be calculated - item.Gross * Percent / 100;
        
        
    }
}