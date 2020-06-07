using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.kaande.pitsariina.Domain.Base;

namespace Domain.App
{
    public class Item : DomainEntityIdMetadata
    {
        public Guid SharingId { get; set; } = default!;
        public Sharing? Sharing { get; set; }

        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gross { get; set; } = default!;

        public ICollection<SharingItem>? SharingItems { get; set; }
    }
}