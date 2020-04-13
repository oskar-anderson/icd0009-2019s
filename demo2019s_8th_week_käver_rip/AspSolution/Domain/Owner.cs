using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Owner : DomainEntity
    {
        [MinLength(1)] [MaxLength(64)] public string FirstName { get; set; } = default!;
        [MinLength(1)] [MaxLength(64)] public string LastName { get; set; } = default!;

        public ICollection<OwnerAnimal>? Animals { get; set; }

        public string FirstLastName => FirstName + " " + LastName;
        public string LastFirstName => LastName + " " + FirstName;
    }
}