using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PaymentMethodDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public DateTime Since { get; set; } = default!;
        public DateTime Until { get; set; } = default!;
    }
}