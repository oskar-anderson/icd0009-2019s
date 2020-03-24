using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityMetadata
    {
        string? CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }

        string? ChangedBy { get; set; }
        DateTime ChangedAt { get; set; }
        

        /* NO SOFT UPDATES/DELETES initially
        public string? DeletedBy { get; set; }
        
        public DateTime? DeletedAt { get; set; }
        */
    }
}