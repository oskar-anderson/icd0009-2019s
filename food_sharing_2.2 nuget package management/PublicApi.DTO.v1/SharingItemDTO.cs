using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class SharingItemDTO
    {
        public Guid Id { get; set; }
        
        public Guid SharingId { get; set; } = default!;
        public SharingDTO? Sharing { get; set; }

        public Guid ItemId { get; set; } = default!;
        public ItemDTO? Item { get; set; }

        [MaxLength(128)][MinLength(1)] public string FriendName { get; set; } = default!;
        
        [Column(TypeName = "decimal(18,4)")] public decimal Percent { get; set; } = default!;

        [Column(TypeName = "decimal(18,4)")] public decimal FriendOwns { get; set; } = default!;    // Will be calculated - item.Gross * Percent / 100;

    }
}