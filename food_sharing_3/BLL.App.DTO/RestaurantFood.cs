using System;
using ee.itcollege.kaande.pitsariina.Contracts.Domain;

namespace BLL.App.DTO
{
    public class RestaurantFood : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid PizzaId { get; set; } = default!;
        public Pizza? Pizza { get; set; }

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public decimal Gross { get; set; } = default!;

    }
}