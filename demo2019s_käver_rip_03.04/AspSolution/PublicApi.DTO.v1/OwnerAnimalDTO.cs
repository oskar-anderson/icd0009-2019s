using System;

namespace PublicApi.DTO.v1
{
    public class OwnerAnimalDTO
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        public OwnerDTO Owner { get; set; } = default!;
        
        public Guid AnimalId { get; set; }
        public AnimalDTO Animal { get; set; } = default!;
        
        public int OwnedPercentage { get; set; }
    }
}