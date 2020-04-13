﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ComponentDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)] public string Name { get; set; } = default!;

        [Range(0, 4)] public int Max { get; set; } = default!;

    }
}