using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PaymentOption
    {
        [Required] public int PaymentOptionId { get; set; }
        
        [Required] 
        [MaxLength(64)]
        public string Option { get; set; }

        /*
        
        Swedbank
        Coop
        SEB
        LHV
        Luminor
        
        */
    }
}