using System;
using DAL.Base;

namespace Domain
{
    public class OwnerAnimal : DomainEntity
    {

        public Guid OwnerId { get; set; }
        public Owner? Owner { get; set; }

        public Guid AnimalId { get; set; }
        public Animal? Animal { get; set; }

        public int OwnedPercentage { get; set; }
        
    }
}