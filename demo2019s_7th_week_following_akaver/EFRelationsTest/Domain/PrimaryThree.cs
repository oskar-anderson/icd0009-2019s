using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class PrimaryThree
    {
        public int Id { get; set; }

        [MaxLength(128)] [MinLength(1)] public string Value { get; set; } = default!;

        // 1:0-1, Fk in principal and dependent entity
        public int? ChildThreeId { get; set; }
        [ForeignKey(nameof(ChildThreeId))]
        public ChildThree? ChildThree { get; set; }
        
    }
}