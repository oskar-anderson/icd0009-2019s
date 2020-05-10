using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    // for display only
    public class OwnerAnimal : OwnerAnimalEdit
    {
        public ICollection<Owner> Owners { get; set; } = default!;
        public ICollection<Animal> Animals { get; set; } = default!;
    }

    // for display only
    public class OwnerAnimalDetail : OwnerAnimalEdit
    {
        public Owner Owner { get; set; } = default!;
        public Animal Animal { get; set; } = default!;
    }

    // from client to server
    public class OwnerAnimalEdit: OwnerAnimalCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class OwnerAnimalCreate
    {
        public Guid OwnerId { get; set; }
        public Guid AnimalId { get; set; }
        public int OwnedPercentage { get; set; }
    }
}