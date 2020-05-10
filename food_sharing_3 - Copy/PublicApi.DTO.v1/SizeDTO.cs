using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class SizeDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)] [MinLength(1)] public string Name { get; set; } = default!;

    }
}