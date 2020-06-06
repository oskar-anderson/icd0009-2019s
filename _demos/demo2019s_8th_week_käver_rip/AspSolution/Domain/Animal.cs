using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Animal : DomainEntity
    {
        [MinLength(1)] [MaxLength(64)] 
        public string AnimalName { get; set; } = default!;
        
        public int? BirthYear { get; set; }

        public ICollection<OwnerAnimal>? Owners { get; set; }
    }
}