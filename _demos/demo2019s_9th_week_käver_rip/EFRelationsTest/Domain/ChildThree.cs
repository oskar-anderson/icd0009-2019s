using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ChildThree
    {
        public int Id { get; set; }

        [MaxLength(128)] [MinLength(1)] 
        public string Value { get; set; }  = default!;

        public int PrimaryThreeId { get; set; }
        [ForeignKey(nameof(PrimaryThreeId))]
        public PrimaryThree? PrimaryThree { get; set; }

    }
}