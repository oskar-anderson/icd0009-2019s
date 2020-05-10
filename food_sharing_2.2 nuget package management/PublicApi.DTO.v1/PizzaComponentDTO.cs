using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PizzaComponentDTO
    {
        public Guid Id { get; set; }
        
        public Guid ComponentId { get; set; } = default!;
        public ComponentDTO? Component { get; set; }

        public Guid? PizzaFinalId { get; set; }
        public PizzaFinalDTO? PizzaFinal { get; set; }
        
        public Guid? PizzaTemplateId { get; set; }
        public PizzaTemplateDTO? PizzaTemplate { get; set; }

        [Range(1, 4, ErrorMessage = "Please enter an amount between 1 and 4")]
        public int Amount { get; set; } = default!;
    }
}