using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)] [MinLength(1)] public string Name { get; set; } = default!;

    }
}