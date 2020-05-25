using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicApi.DTO.v1
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        
        public Guid SharingId { get; set; } = default!;
        public SharingDTO? Sharing { get; set; }
        
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;

    }
}