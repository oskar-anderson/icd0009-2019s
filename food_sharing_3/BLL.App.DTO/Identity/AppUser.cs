using System;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace BLL.App.DTO.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        
        // public virtual ICollection<Sharing>? Sharings { get; set; }
        // public virtual ICollection<UserLocation>? UserLocations { get; set; }
        // public virtual ICollection<Cart>? Carts { get; set; }
        // public virtual ICollection<Person>? Persons { get; set; }

    }
}