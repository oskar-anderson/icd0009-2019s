using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class PrimaryOne
    {
        public int Id { get; set; }
        [MaxLength(128)] [MinLength(1)] public string Value { get; set; } = default!;

        // 1:0-1, Fk in principal entity
        public int ChildOneId { get; set; }
        public ChildOne? ChildOne { get; set; }
    }
}