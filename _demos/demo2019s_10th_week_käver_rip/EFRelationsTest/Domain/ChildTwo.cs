using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Domain
{
    public class ChildTwo
    {
        public int Id { get; set; }

        [MaxLength(128)] 
        [MinLength(1)] 
        public string Value { get; set; }  = default!;

        public int PrimaryTwoId { get; set; }
        public PrimaryTwo? PrimaryTwo { get; set; }
    }
}