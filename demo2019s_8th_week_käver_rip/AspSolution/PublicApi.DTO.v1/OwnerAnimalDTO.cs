using System;

namespace PublicApi.DTO.v1
{
    public class OwnerAnimalDTO
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        public Guid AnimalId { get; set; }
        
        public int OwnedPercentage { get; set; }
    }
}