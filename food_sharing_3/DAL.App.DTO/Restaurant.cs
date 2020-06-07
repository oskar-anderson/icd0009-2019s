using System;
using System.Collections.Generic;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace DAL.App.DTO
{
    public class Restaurant : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;

        public string Location { get; set; } = default!;

        public string Telephone { get; set; } = default!;

        public string OpenTime { get; set; } = default!;

        public string OpenNotification { get; set; } = default!;

        public ICollection<Cart>? Carts { get; set; }
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; }

    }
}