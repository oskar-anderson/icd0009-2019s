using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class RestaurantDTO
    {
        public Guid Id { get; set; }
        
        [MinLength(1)] [MaxLength(64)] public string Name { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string Location { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string Telephone { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string OpenTime { get; set; } = default!;

        [MinLength(1)] [MaxLength(64)] public string OpenNotification { get; set; } = default!;

    }
}