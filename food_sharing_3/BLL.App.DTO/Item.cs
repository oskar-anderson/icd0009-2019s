using System;
using System.Collections.Generic;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Item : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid SharingId { get; set; } = default!;
        public Sharing? Sharing { get; set; }

        public string Name { get; set; } = default!;
        
        public decimal Gross { get; set; } = default!;

        public ICollection<SharingItem>? SharingItems { get; set; }
    }
}