using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain
{
    public class Invoice
    {
        [Required] public int InvoiceId { get; set; }

        [Required] public int PersonId { get; set; }
        public Person Person { get; set; }

        [Required] public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required] public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public DateTimeOffset CreationTimestamp
        {
            get { return CreationTimestamp; } 
            set { CreationTimestamp = DateTimeOffset.Now; }
        }

        [Required]
        [MaxLength(32)]
        public string InvoiceCode
        {
            get { return InvoiceCode; }
            set { InvoiceCode = RandomString(20); }
        }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required] public decimal TotalNet { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required] public decimal TotalTax { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        [Required] public decimal TotalGross { get; set; }
        
        
        
        
        
        
        
        private static readonly Random Random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}