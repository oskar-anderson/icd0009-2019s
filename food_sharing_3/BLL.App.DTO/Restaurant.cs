using System;
using System.Collections.Generic;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Restaurant : Contracts.Domain.IDomainEntityId
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