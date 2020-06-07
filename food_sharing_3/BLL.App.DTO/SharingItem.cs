using System;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace BLL.App.DTO
{
    public class SharingItem : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid SharingId { get; set; } = default!;
        public Sharing? Sharing { get; set; }

        public Guid ItemId { get; set; } = default!;
        public Item? Item { get; set; }

        public string FriendName { get; set; } = default!;
        
        public decimal Percent { get; set; } = default!;
        
        public decimal FriendOwns { get; set; } = default!;    // Will be calculated - item.Gross * Percent / 100;

    }
}