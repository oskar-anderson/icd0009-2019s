using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Restaurant : DomainEntityIdMetadata
    {
        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string Location { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string Telephone { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string OpenTime { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string OpenNotification { get; set; } = default!;

        public ICollection<Cart>? Carts { get; set; } 
        public ICollection<Invoice>? Invoices { get; set; } 
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; } 
        public ICollection<ComponentPrice>? ComponentPrices { get; set; } 
        
        /*
        Pitsa Riina
        Endla 76
        5724871
        E-P, 12:00-23:00
        10., 12. aprill, 1., 31. mai
        */
    }
}