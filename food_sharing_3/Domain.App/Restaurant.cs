using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.kaande.pitsariina.Domain.Base;

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
        public ICollection<RestaurantFood>? RestaurantFoods { get; set; }
    }
}