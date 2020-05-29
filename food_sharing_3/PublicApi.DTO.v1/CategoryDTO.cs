using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        
        [MinLength(1)] [MaxLength(64)]  public string Name { get; set; } = default!;
    }
}